using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PoMoyka.Backend.Application.Services.User
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public GetUserQuery() { }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetUserQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var user = await _context.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Authenticated user with ID {userId} not found in database.");
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
