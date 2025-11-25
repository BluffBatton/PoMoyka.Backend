using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Statistics
{
    public class GetTopCentersStatisticsQuery : IRequest<TopCentersStatisticsDto>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GetTopCentersStatisticsQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }

    public class GetTopCentersStatisticsQueryHandler : IRequestHandler<GetTopCentersStatisticsQuery, TopCentersStatisticsDto>
    {
        private readonly IApplicationDbContext _context;
        public GetTopCentersStatisticsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TopCentersStatisticsDto> Handle(GetTopCentersStatisticsQuery request, CancellationToken cancellationToken)
        {
            var from = request.From.Date;
            var to = request.To.Date.AddDays(1);

            var bookingsQuery = _context.Bookings
                .Include(b => b.CenterService)
                    .ThenInclude(cs => cs.Center)
                .Include(b => b.Transaction)
                .Where(b => b.BookedTime >= from && b.BookedTime < to);

            var centers = await bookingsQuery
                .GroupBy(b => new
                {
                    CenterId = b.CenterService.Center.Id,
                    CenterName = b.CenterService.Center.Name
                })
                .Select(g => new CenterSalesItemDto
                {
                    CenterName = g.Key.CenterName,
                    Quantity = g.Count()
                })
                .OrderByDescending(c => c.Quantity)
                .ToListAsync(cancellationToken);

            var totalSales = centers.Sum(c => c.Quantity);

            var totalCash = await bookingsQuery
                .Where(b => b.Transaction != null)
                .SumAsync(b => b.Transaction!.Amount, cancellationToken);

            return new TopCentersStatisticsDto
            {
                Centers = centers,
                TotalSales = totalSales,
                TotalCash = totalCash
            };
        }
    }
}
