using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Service
{
    public class GetAllServiceQuery : IRequest<List<ServiceDto>>{}

    public class GetAllServiceQueryHandler : IRequestHandler<GetAllServiceQuery, List<ServiceDto>>
    {
        IApplicationDbContext _context;
        IMapper _mapper;

        public GetAllServiceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServiceDto>> Handle(GetAllServiceQuery request, CancellationToken cancellationToken)
        {
            return await _context.Services
                .AsNoTracking()
                .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
