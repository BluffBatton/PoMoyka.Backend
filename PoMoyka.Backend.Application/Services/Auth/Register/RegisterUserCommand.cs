using MediatR;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Contracts.DTOs.AuthDTOs;

namespace PoMoyka.Backend.Application.Services.Auth.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public RegisterDto Register { get; set; }
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

            var userExists = await _context.Users
                .AnyAsync(u => u.Email == request.Register.User.Email, cancellationToken);

            if (userExists)
                throw new InvalidOperationException($"User with email {request.Register.User.Email} already exists");

            // Create user
            var user = _mapper.Map<PoMoyka.Backend.Domain.Entities.User>(request.Register.User);

            // Hash password using BCrypt
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Register.User.PasswordHash);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var userFromDb = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Register.User.Email, cancellationToken);

            // Create car with required properties
            var car = _mapper.Map<PoMoyka.Backend.Domain.Entities.Car>(request.Register.Car);
            car.UserId = userFromDb.Id;

            await _context.Cars.AddAsync(car, cancellationToken); 
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
