using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.Application.Services.CenterService
{
    public class DeleteCenterServiceCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteCenterServiceCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCenterServiceCommandHandler : IRequestHandler<DeleteCenterServiceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCenterServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCenterServiceCommand request, CancellationToken cancellationToken)
        {
            var centerService = await _context.CenterServices
                .FirstOrDefaultAsync(cs => cs.Id == request.Id, cancellationToken);

            if (centerService == null)
            {
                throw new Exception($"CenterService with ID {request.Id} not found");
            }

            _context.CenterServices.Remove(centerService);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

