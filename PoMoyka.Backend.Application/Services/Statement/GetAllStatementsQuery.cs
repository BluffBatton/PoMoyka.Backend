using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Statement
{
    public class GetAllStatementsQuery : IRequest<List<StatementDto>>
    {
    }
    public class GetAllStatementsQueryHandler : IRequestHandler<GetAllStatementsQuery, List<StatementDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStatementsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StatementDto>> Handle(GetAllStatementsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Statements
                .AsNoTracking()
                .ProjectTo<StatementDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
