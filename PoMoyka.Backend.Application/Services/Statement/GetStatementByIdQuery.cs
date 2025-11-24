using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Statement
{
    public class GetStatementByIdQuery : IRequest<StatementDto>
    {
        public Guid StatementId { get; set; }
        public GetStatementByIdQuery(Guid id)
        {
            StatementId = id;
        }
    }

    public class GetStatementByIdQueryHandler : IRequestHandler<GetStatementByIdQuery, StatementDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStatementByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StatementDto> Handle(GetStatementByIdQuery request, CancellationToken cancellationToken)
        {
            var statement = await _context.Statements
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == request.StatementId, cancellationToken); 
            if (statement == null)
            {
                throw new Exception($"Statement with Id {request.StatementId} doesn't exists");
            }

            statement.Status = (Domain.Enums.StatementStatus)StatementStatus.Read;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<StatementDto>(statement);
        }
    }
}
