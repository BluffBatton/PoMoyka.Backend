using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.Application.Services.Service
{
    public class DeleteServiceCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FindAsync(request.Id, cancellationToken);
            if(service == null)
            {
                throw new Exception($"Service with Id '{request.Id}'");
            }

            var isUsed = await _context.TypeServices
                .AnyAsync(ts => ts.ServiceId == request.Id, cancellationToken);
            if (isUsed)
            {
                throw new InvalidOperationException($"Can't delete service: it is currently used in Type Services");
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
