using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class GetAllCentersQuery : IRequest<List<CenterMapDto>>
    {

    }

    public class GetAllCentersQueryHandler : IRequestHandler<GetAllCentersQuery, List<CenterMapDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCentersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CenterMapDto>> Handle(GetAllCentersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Centers
                .AsNoTracking()
                .ProjectTo<CenterMapDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
