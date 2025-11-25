using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.Application.Services.Employee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid EmployeeId { get; }

        public DeleteEmployeeCommand(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.EmployeeId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Employee with Id {request.EmployeeId} not found");
            }

            var center = await _context.Centers
                .FirstOrDefaultAsync(c => c.UserId == request.EmployeeId, cancellationToken);

            if (center != null)
            {
                center.UserId = null;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
