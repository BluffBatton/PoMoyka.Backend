using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class GetAllCentersQuery : IRequest<List<CenterDto>>
    {

    }

    public class GetAllCentersQueryHandler : IRequestHandler<GetAllCentersQuery, List<CenterDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCentersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CenterDto>> Handle(GetAllCentersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Centers
                .AsNoTracking()
                .ProjectTo<CenterDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
