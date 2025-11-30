using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class GetMyBookingsQuery : IRequest<List<BookingDetailedDto>>
    {
        public BookingStatus? Status { get; }

        public GetMyBookingsQuery(BookingStatus? status = null)
        {
            Status = status;
        }
    }

    public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, List<BookingDetailedDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;

        public GetMyBookingsQueryHandler(IApplicationDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<BookingDetailedDto>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (!userId.HasValue || userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var query = _context.Bookings
                .AsNoTracking()
                .Where(b => b.UserId == userId.Value);

            // Фильтр по статусу
            if (request.Status.HasValue)
            {
                var domainStatus = (Domain.Enums.BookingStatus)request.Status.Value;
                query = query.Where(b => b.Status == domainStatus);
            }

            var bookings = await query
                .Include(b => b.User)
                .Include(b => b.CenterService)
                    .ThenInclude(cs => cs.Center)
                .Include(b => b.CenterService)
                    .ThenInclude(cs => cs.TypeService)
                        .ThenInclude(ts => ts.Service)
                .Include(b => b.Transaction)
                    .ThenInclude(t => t.Rating)
                .OrderByDescending(b => b.CreatedAt)
                .Select(b => new BookingDetailedDto
                {
                    Id = b.Id,
                    BookedTime = b.BookedTime,
                    Status = (BookingStatus)b.Status,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    UserId = b.UserId,
                    UserFirstName = b.User.FirstName,
                    UserLastName = b.User.LastName,
                    UserEmail = b.User.Email,
                    CenterId = b.CenterService.CenterId,
                    CenterName = b.CenterService.Center.Name,
                    CenterAddress = b.CenterService.Center.Address,
                    CenterServiceId = b.CenterServiceId,
                    ServiceName = b.CenterService.TypeService.Service.Name,
                    ServiceDescription = b.CenterService.TypeService.Service.Description,
                    CarType = (CarType)b.CenterService.TypeService.CarType,
                    Price = b.CenterService.Price,
                    TransactionId = b.Transaction != null ? b.Transaction.Id : null,
                    TransactionAmount = b.Transaction != null ? b.Transaction.Amount : null,
                    RatingId = b.Transaction != null && b.Transaction.Rating != null ? b.Transaction.Rating.Id : null,
                    RatingValue = b.Transaction != null && b.Transaction.Rating != null ? (int)b.Transaction.Rating.RatingNumber + 1 : null // One=0 -> 1, Two=1 -> 2, etc.
                })
                .ToListAsync(cancellationToken);

            return bookings;
        }
    }
}

