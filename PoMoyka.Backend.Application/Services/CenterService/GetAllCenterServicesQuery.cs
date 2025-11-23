using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.CenterService
{
    public class GetAllCenterServicesQuery : IRequest<List<CenterServiceDto>>
    {
        public Guid CenterId { get; }

        public GetAllCenterServicesQuery(Guid centerId)
        {
            CenterId = centerId;
        }
    }

    public class GetAllCenterServicesQueryHandler : IRequestHandler<GetAllCenterServicesQuery, List<CenterServiceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCenterServicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CenterServiceDto>> Handle(GetAllCenterServicesQuery request, CancellationToken cancellationToken)
        {
            var centerServices = await _context.CenterServices
                .AsNoTracking()
                .Where(cs => cs.CenterId == request.CenterId)
                .Include(cs => cs.TypeService)
                    .ThenInclude(ts => ts.Service)
                .Select(cs => new CenterServiceDto
                {
                    Id = cs.Id,
                    CenterId = cs.CenterId,
                    TypeServiceId = cs.TypeServiceId,
                    Price = cs.Price,
                    ServiceName = cs.TypeService.Service.Name,
                    CarType = cs.TypeService.CarType.ToString(),
                    CreatedAt = cs.CreatedAt,
                    UpdatedAt = cs.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            return centerServices;
        }
    }
}

