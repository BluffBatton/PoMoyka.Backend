using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class GetCenterPricelistQuery : IRequest<CenterPricelistDto>
    {
        public Guid CenterId { get; set; }

        public GetCenterPricelistQuery(Guid centerId)
        {
            CenterId = centerId;
        }
    }

    public class GetCenterPricelistQueryHandler : IRequestHandler<GetCenterPricelistQuery, CenterPricelistDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;
        public GetCenterPricelistQueryHandler(IApplicationDbContext context, IUserContextService service)
        {
            _context = context;
            _userContextService = service;
        }

        public async Task<CenterPricelistDto> Handle(GetCenterPricelistQuery request, CancellationToken cancellationToken)
        {
            var center = await _context.Centers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.CenterId, cancellationToken);

            if (center == null)
            {
                throw new Exception("Center was not found");
            }

            // Пытаемся получить userId текущего пользователя (может быть null если не авторизован)
            var userId = _userContextService.GetCurrentUserId();

            // Определяем тип фильтра по машине
            Domain.Enums.CarType? filterCarType = null;
            if (userId != Guid.Empty)
            {
                var userCar = await _context.Cars
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
                
                if (userCar != null)
                {
                    filterCarType = userCar.CarType;
                }
            }

            // Строим запрос с учетом фильтра
            IQueryable<Domain.Entities.CenterService> query = _context.CenterServices
                .AsNoTracking()
                .Where(cs => cs.CenterId == request.CenterId)
                .Include(cs => cs.TypeService)
                    .ThenInclude(ts => ts.Service);

            if (filterCarType.HasValue)
            {
                query = query.Where(cs => cs.TypeService.CarType == filterCarType.Value);
            }

            var pricedServices = await query
                .Select(cs => new PricedServiceDto
                {
                    CenterServiceId = cs.Id,
                    ServiceName = cs.TypeService.Service.Name,
                    CarType = cs.TypeService.CarType.ToString(),
                    Price = cs.Price,
                    Description = cs.TypeService.Service.Description
                })
                .ToListAsync(cancellationToken);

            return new CenterPricelistDto
            {
                CenterId = center.Id,
                CenterName = center.Name,
                Address = center.Address,
                Services = pricedServices
            };
               
        }
    }

}
