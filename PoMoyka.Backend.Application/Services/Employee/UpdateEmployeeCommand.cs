using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        public Guid EmployeeId { get; }
        public EmployeeUpdateDto Dto { get; }

        public UpdateEmployeeCommand(Guid employeeId, EmployeeUpdateDto dto)
        {
            EmployeeId = employeeId;
            Dto = dto;
        }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateEmployeeCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.EmployeeId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Employee with Id {request.EmployeeId} not found");
            }

            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != user.Email)
            {
                var emailExists = await _context.Users
                    .AnyAsync(u => u.Email == dto.Email, cancellationToken);

                if (emailExists)
                {
                    throw new Exception($"User with email '{dto.Email}' already exists.");
                }
            }

            if (dto.CenterId.HasValue)
            {
                var newCenterId = dto.CenterId.Value;

                var newCenter = await _context.Centers
                    .FirstOrDefaultAsync(c => c.Id == newCenterId, cancellationToken);

                if (newCenter == null)
                {
                    throw new Exception($"Center with Id '{newCenterId}' does not exist.");
                }

                var currentCenter = await _context.Centers
                    .FirstOrDefaultAsync(c => c.UserId == user.Id, cancellationToken);

                if (currentCenter == null || currentCenter.Id != newCenter.Id)
                {
                    if (currentCenter != null)
                    {
                        currentCenter.UserId = null;
                    }

                    if (newCenter.UserId.HasValue && newCenter.UserId.Value != user.Id)
                    {
                        throw new Exception($"Center '{newCenterId}' is already assigned to another user.");
                    }

                    newCenter.UserId = user.Id;
                }
            }

            _mapper.Map(dto, user);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = _passwordHasher.Hash(dto.Password);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
