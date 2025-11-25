using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Employee
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public Guid EmployeeId { get; }

        public GetEmployeeByIdQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(
            GetEmployeeByIdQuery request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == request.EmployeeId && u.Role == Role.Employee)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (employee == null)
            {
                throw new Exception($"Employee with Id {request.EmployeeId} not found");
            }

            return employee;
        }
    }
}
