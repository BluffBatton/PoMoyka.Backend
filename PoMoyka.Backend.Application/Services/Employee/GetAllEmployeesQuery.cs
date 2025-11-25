using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Employee.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
    {
    }

    public class GetAllEmployeesQueryHandler
        : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> Handle(
            GetAllEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Role == Role.Employee) 
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
