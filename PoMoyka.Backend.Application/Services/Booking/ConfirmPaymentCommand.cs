using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public ConfirmPaymentCommandHandler(IApplicationDbContext context, ILiqPayService liqPayService)
        {
            _context = context;
            _liqPayService = liqPayService;
        }

        public async Task<Unit> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            // Проверка подписи от LiqPay
            if (!_liqPayService.VerifyCallback(request.CallbackDto.Data, request.CallbackDto.Signature))
            {
                throw new Exception("Invalid LiqPay signature");
            }

            // Парсим данные от LiqPay (нужно реализовать метод в ILiqPayService)
            var paymentData = _liqPayService.ParseCallbackData(request.CallbackDto.Data);

            // Получаем bookingId из данных платежа
            var bookingId = paymentData.BookingId;

            var booking = await _context.Bookings
                .Include(b => b.CenterService)
                .FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);

            if (booking == null)
            {
                throw new Exception($"Booking with ID {bookingId} not found");
            }

            // Проверяем статус платежа
            if (paymentData.Status == "success")
            {
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
            }
            else // failure, error, pending или любой другой статус
            {
                // Оплата не удалась или ожидает подтверждения - отменяем бронирование
                booking.Status = BookingStatus.Cancelled;
                booking.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

