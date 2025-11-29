using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.AuthDTOs;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class ConfirmPaymentCommand : IRequest<Unit>
    {
        public LiqPayCallbackDto CallbackDto { get; }

        public ConfirmPaymentCommand(LiqPayCallbackDto callbackDto)
        {
            CallbackDto = callbackDto;
        }
    }

    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILiqPayService _liqPayService;
        private readonly ILogger<ConfirmPaymentCommandHandler> _logger;

        public ConfirmPaymentCommandHandler(
            IApplicationDbContext context, 
            ILiqPayService liqPayService,
            ILogger<ConfirmPaymentCommandHandler> logger)
        {
            _context = context;
            _liqPayService = liqPayService;
            _logger = logger;
        }

        public async Task<Unit> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {

            // Проверка подписи от LiqPay
            if (!_liqPayService.VerifyCallback(request.CallbackDto.Data, request.CallbackDto.Signature))
            {
                _logger.LogError(" Invalid LiqPay signature!");
                throw new Exception("Invalid LiqPay signature");
            }

            _logger.LogInformation("LiqPay signature verified");

            // Парсим данные от LiqPay
            var paymentData = _liqPayService.ParseCallbackData(request.CallbackDto.Data);

            _logger.LogInformation("Parsed payment data: BookingId={BookingId}, Status={Status}, Amount={Amount}", 
                paymentData.BookingId, paymentData.Status, paymentData.Amount);

            // Получаем bookingId из данных платежа
            var bookingId = paymentData.BookingId;

            var booking = await _context.Bookings
                .Include(b => b.CenterService)
                .FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);

            if (booking == null)
            {
                _logger.LogError("Booking with ID {BookingId} not found!", bookingId);
                throw new Exception($"Booking with ID {bookingId} not found");
            }

            _logger.LogInformation("Found booking: Current status={Status}", booking.Status);

            // Проверяем статус платежа
            if (paymentData.Status == "success")
            {
                _logger.LogInformation("Payment successful! Updating booking to Done");
                
                // Оплата успешна - меняем статус на Done
                booking.Status = BookingStatus.Done;
                booking.UpdatedAt = DateTime.UtcNow;

                // Создаем транзакцию
                var transaction = new Domain.Entities.Transaction
                {
                    Amount = booking.CenterService.Price,
                    BookingId = booking.Id,
                    Booking = booking,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Transactions.AddAsync(transaction, cancellationToken);
                _logger.LogInformation("Transaction created with Amount={Amount}", booking.CenterService.Price);
            }
            else // failure, error, pending или любой другой статус
            {
                _logger.LogWarning("Payment not successful! Status={Status}. Cancelling booking", paymentData.Status);
                
                // Оплата не удалась или ожидает подтверждения - отменяем бронирование
                booking.Status = BookingStatus.Cancelled;
                booking.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Booking updated successfully. New status={Status}", booking.Status);
            _logger.LogInformation("LiqPay Callback Processing Complete ===");

            return Unit.Value;
        }
    }
}


