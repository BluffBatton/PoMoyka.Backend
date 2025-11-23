using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class DeleteCenterCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteCenterCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCenterCommandHandler : IRequestHandler<DeleteCenterCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCenterCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCenterCommand request, CancellationToken cancellationToken)
        {
            var center = await _context.Centers
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (center == null)
            {
                throw new Exception($"Center with ID {request.Id} not found");
            }

            _context.Centers.Remove(center);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

