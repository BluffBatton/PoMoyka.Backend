using MediatR;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.UserDTOs;
using PoMoyka.Backend.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PoMoyka.Backend.Application.Services.Auth.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public UserCreateDto User { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user with same email already exists
            var userExists = await _context.Users
                .AnyAsync(u => u.Email == request.User.Email, cancellationToken);

            if (userExists)
                throw new InvalidOperationException($"User with email {request.User.Email} already exists");

            // Create user
            var user = _mapper.Map<User>(request.User);

            // Hash password using BCrypt
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.User.PasswordHash);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
