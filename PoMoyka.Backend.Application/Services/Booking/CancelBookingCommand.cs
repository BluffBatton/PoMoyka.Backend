using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class CancelBookingCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public CancelBookingCommand(Guid id)
        {
            Id = id;
        }
    }

    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;

        public CancelBookingCommandHandler(IApplicationDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<Unit> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (!userId.HasValue || userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (booking == null)
            {
                throw new Exception($"Booking with ID {request.Id} not found");
            }

            // Проверка прав доступа: пользователь может отменить только свое бронирование
            if (booking.UserId != userId.Value)
            {
                throw new UnauthorizedAccessException("You can only cancel your own bookings");
            }

            // Нельзя отменить уже завершенное или отмененное бронирование
            if (booking.Status == BookingStatus.Done)
            {
                throw new Exception("Cannot cancel a completed booking");
            }

            if (booking.Status == BookingStatus.Cancelled)
            {
                throw new Exception("Booking is already cancelled");
            }

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

