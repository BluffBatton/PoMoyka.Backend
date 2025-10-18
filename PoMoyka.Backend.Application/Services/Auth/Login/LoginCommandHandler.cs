using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.Application.Services.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UserData;
            var user = await _context.Users.
                FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);
            if(user is null)
            {
                throw new Exception("Invalid email or password");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if(!isPasswordValid)
            {
                throw new Exception("Invalid email or password");
            }

            var token = _jwtService.GenerateAccessToken(user);
            return token;
        }
    }
}
