using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Application.Services.Employee
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public EmployeeCreateDto Dto { get; set; }
        public CreateEmployeeCommand(EmployeeCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public CreateEmployeeCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == dto.Email, cancellationToken);

            if (emailExists)
                throw new InvalidOperationException($"User with email '{dto.Email}' already exists.");

            var centerExists = await _context.Centers
                .AnyAsync(c => c.Id == dto.CenterId, cancellationToken);

            if (!centerExists)
            {
                throw new InvalidOperationException($"Center with Id '{dto.CenterId}' does not exist.");

            }

            var user = _mapper.Map<Domain.Entities.User>(dto);

            user.PasswordHash = _passwordHasher.Hash(dto.Password);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
