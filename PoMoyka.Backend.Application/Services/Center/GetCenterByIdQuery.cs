using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class GetCenterByIdQuery : IRequest<CenterDetailedDto>
    {
        public Guid Id { get; }

        public GetCenterByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetCenterByIdQueryHandler : IRequestHandler<GetCenterByIdQuery, CenterDetailedDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCenterByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CenterDetailedDto> Handle(GetCenterByIdQuery request, CancellationToken cancellationToken)
        {
            var center = await _context.Centers
                .AsNoTracking()
                .Include(c => c.CenterServices)
                    .ThenInclude(cs => cs.TypeService)
                        .ThenInclude(ts => ts.Service)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (center == null)
            {
                throw new Exception($"Center with ID {request.Id} not found");
            }

            var dto = _mapper.Map<CenterDetailedDto>(center);

            // Загружаем услуги с ценами
            dto.Services = center.CenterServices?.Select(cs => new PricedServiceDto
            {
                CenterServiceId = cs.Id,
                ServiceName = cs.TypeService.Service.Name,
                CarType = cs.TypeService.CarType.ToString(),
                Price = cs.Price,
                Description = cs.TypeService.Service.Description
            }).ToList() ?? new List<PricedServiceDto>();

            // Вычисляем средний рейтинг центра
            var centerServiceIds = center.CenterServices?.Select(cs => cs.Id).ToList() ?? new List<Guid>();

            if (centerServiceIds.Any())
            {
                // Получаем рейтинги через Transaction -> Booking -> CenterService
                var ratings = await _context.Ratings
                    .AsNoTracking()
                    .Include(r => r.Transaction)
                        .ThenInclude(t => t.Booking)
                    .Where(r => centerServiceIds.Contains(r.Transaction.Booking.CenterServiceId))
                    .Select(r => (int)r.RatingNumber + 1) // One=0 -> 1, Two=1 -> 2, Three=2 -> 3, Four=3 -> 4, Five=4 -> 5
                    .ToListAsync(cancellationToken);

                if (ratings.Any())
                {
                    dto.AverageRating = Math.Round(ratings.Average(), 2);
                    dto.TotalRatings = ratings.Count;
                }
                else
                {
                    dto.AverageRating = null;
                    dto.TotalRatings = 0;
                }
            }
            else
            {
                dto.AverageRating = null;
                dto.TotalRatings = 0;
            }

            return dto;
        }
    }
}

