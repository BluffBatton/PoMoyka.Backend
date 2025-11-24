using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Statement
{
    public class CreateStatementCommand : IRequest<Guid>
    {
        public StatementCreateDto Dto { get; set; }
        public CreateStatementCommand(StatementCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CreateStatementCommandHandler(IApplicationDbContext context, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if(userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);

            var statement = _mapper.Map<Domain.Entities.Statement>(request.Dto);

            statement.UserId = userId.Value;
            statement.CreatedAt = DateTime.UtcNow;

            await _context.Statements.AddAsync(statement, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return statement.Id;
        }
    }
}
