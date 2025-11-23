using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class CompleteBookingCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public CompleteBookingCommand(Guid id)
        {
            Id = id;
        }
    }

    public class CompleteBookingCommandHandler : IRequestHandler<CompleteBookingCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public CompleteBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CompleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .Include(b => b.CenterService)
                .Include(b => b.Transaction)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (booking == null)
            {
                throw new Exception($"Booking with ID {request.Id} not found");
            }

            // Проверка статуса
            if (booking.Status == BookingStatus.Cancelled)
            {
                throw new Exception("Cannot complete a cancelled booking");
            }

            if (booking.Status == BookingStatus.Done)
            {
                // Уже завершено
                return Unit.Value;
            }

            // Меняем статус на Done
            booking.Status = BookingStatus.Done;
            booking.UpdatedAt = DateTime.UtcNow;

            // Если еще нет транзакции - создаем
            if (booking.Transaction == null)
            {
                var transaction = new Domain.Entities.Transaction
                {
                    Amount = booking.CenterService.Price,
                    BookingId = booking.Id,
                    Booking = booking,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Transactions.AddAsync(transaction, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

