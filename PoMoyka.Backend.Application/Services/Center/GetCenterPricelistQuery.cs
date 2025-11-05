using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class GetCenterPricelistQuery : IRequest<CenterPricelistDto>
    {
        public Guid CenterId { get; set; }
        public Guid UserId { get; set; }

        public GetCenterPricelistQuery(Guid centerId, Guid userId)
        {
            CenterId = centerId;
            UserId = userId;
        }
    }

    public class GetCenterPricelistQueryHandler : IRequestHandler<GetCenterPricelistQuery, CenterPricelistDto>
    {
        private readonly IApplicationDbContext _context;
        public GetCenterPricelistQueryHandler(IApplicationDbContext context)
        {
            _context = context;
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

            var userCar = await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);
            if (userCar == null) 
            {
                throw new Exception("Car was not found");
            }

            var userCarType = userCar.CarType;

            var pricedServices = await _context.CenterServices
                .AsNoTracking()
                .Where(cs => cs.CenterId == request.CenterId)
                .Include(cs => cs.TypeService)
                    .ThenInclude(ts => ts.Service)
                .Where(cs => cs.TypeService.CarType == userCarType)
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
