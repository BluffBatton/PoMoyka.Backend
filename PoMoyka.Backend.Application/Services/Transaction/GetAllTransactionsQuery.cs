using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Transaction
{
    public class GetAllTransactionsQuery : IRequest<List<TransactionDto>>
    {
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTransactionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
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

