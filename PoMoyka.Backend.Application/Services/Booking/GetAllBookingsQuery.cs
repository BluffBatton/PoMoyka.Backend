using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class GetAllBookingsQuery : IRequest<List<BookingDto>>
    {
        public BookingStatus? Status { get; }
        public Guid? CenterId { get; }
        public Guid? UserId { get; }

        public GetAllBookingsQuery(BookingStatus? status = null, Guid? centerId = null, Guid? userId = null)
        {
            Status = status;
            CenterId = centerId;
            UserId = userId;
        }
    }

    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<BookingDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllBookingsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Bookings.AsNoTracking();

            // Фильтр по статусу
            if (request.Status.HasValue)
            {
                var domainStatus = (Domain.Enums.BookingStatus)request.Status.Value;
                query = query.Where(b => b.Status == domainStatus);
            }

            // Фильтр по центру
            if (request.CenterId.HasValue)
            {
                query = query
                    .Include(b => b.CenterService)
                    .Where(b => b.CenterService.CenterId == request.CenterId.Value);
            }

            // Фильтр по пользователю
            if (request.UserId.HasValue)
            {
                query = query.Where(b => b.UserId == request.UserId.Value);
            }

            var bookings = await query
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    BookedTime = b.BookedTime,
                    Status = (BookingStatus)b.Status,
                    UserId = b.UserId,
                    CenterServiceId = b.CenterServiceId,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return bookings;
        }
    }
}

