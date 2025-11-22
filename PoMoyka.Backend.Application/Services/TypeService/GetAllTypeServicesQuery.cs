using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.TypeService
{
    public class GetAllTypeServicesQuery : IRequest<List<TypeServiceDto>> { }

    public class GetAllTypeServicesQueryHandler : IRequestHandler<GetAllTypeServicesQuery, List<TypeServiceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTypeServicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TypeServiceDto>> Handle(GetAllTypeServicesQuery request, CancellationToken cancellationToken)
        {
            return await _context.TypeServices
                .AsNoTracking()
                .Include(ts => ts.Service)
                .Select(ts => new TypeServiceDto
                {
                    Id = ts.Id,
                    ServiceId = ts.ServiceId,
                    ServiceName = ts.Service != null ? ts.Service.Name : "",
                    CarType = (Contracts.DTOs.Enums.CarType)ts.CarType
                })
                .ToListAsync(cancellationToken);
        }
    }
}

