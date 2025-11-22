using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.Application.Services.TypeService
{
    public class DeleteTypeServiceCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteTypeServiceCommandHandler : IRequestHandler<DeleteTypeServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTypeServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTypeServiceCommand request, CancellationToken cancellationToken)
        {
            var typeService = await _context.TypeServices
                .FirstOrDefaultAsync(ts => ts.Id == request.Id, cancellationToken);

            if (typeService == null)
            {
                throw new Exception($"TypeService with Id '{request.Id}' was not found");
            }

            // Check if used in CenterServices
            var isUsed = await _context.CenterServices
                .AnyAsync(cs => cs.TypeServiceId == request.Id, cancellationToken);

            if (isUsed)
            {
                throw new InvalidOperationException($"Can't delete TypeService: it is currently used in CenterServices");
            }

            _context.TypeServices.Remove(typeService);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

