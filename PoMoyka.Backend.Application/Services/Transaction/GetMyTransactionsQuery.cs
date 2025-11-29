using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Transaction
{
    public class GetMyTransactionsQuery : IRequest<List<TransactionDto>>
    {
    }

    public class GetMyTransactionsQueryHandler : IRequestHandler<GetMyTransactionsQuery, List<TransactionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;

        public GetMyTransactionsQueryHandler(
            IApplicationDbContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<TransactionDto>> Handle(GetMyTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (!userId.HasValue || userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var transactions = await _context.Transactions
                .AsNoTracking()
                .Include(t => t.Booking)
                    .ThenInclude(b => b.User)
                .Include(t => t.Booking)
                    .ThenInclude(b => b.CenterService)
                        .ThenInclude(cs => cs.Center)
                .Include(t => t.Booking)
                    .ThenInclude(b => b.CenterService)
                        .ThenInclude(cs => cs.TypeService)
                            .ThenInclude(ts => ts.Service)
                .Include(t => t.Rating)
                .Where(t => t.Booking.UserId == userId.Value)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    CreatedAt = t.CreatedAt,

                    // Booking info
                    BookingId = t.BookingId,
                    BookedTime = t.Booking.BookedTime,
                    BookingStatus = (Contracts.DTOs.Enums.BookingStatus)(int)t.Booking.Status,

                    // User info
                    UserId = t.Booking.UserId,
                    UserFirstName = t.Booking.User.FirstName,
                    UserLastName = t.Booking.User.LastName,
                    UserEmail = t.Booking.User.Email,

                    // Center info
                    CenterId = t.Booking.CenterService.CenterId,
                    CenterName = t.Booking.CenterService.Center.Name,

                    // Service info
                    ServiceName = t.Booking.CenterService.TypeService.Service.Name,
                    CarType = t.Booking.CenterService.TypeService.CarType.ToString(),

                    // Rating info
                    RatingId = t.Rating != null ? t.Rating.Id : null,
                    RatingValue = t.Rating != null ? (int)t.Rating.RatingNumber : null
                })
                .ToListAsync(cancellationToken);

            return transactions;
        }
    }
}

