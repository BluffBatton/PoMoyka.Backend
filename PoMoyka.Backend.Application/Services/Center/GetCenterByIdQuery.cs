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
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (center == null)
            {
                throw new Exception($"Center with ID {request.Id} not found");
            }

            return _mapper.Map<CenterDetailedDto>(center);
        }
    }
}

