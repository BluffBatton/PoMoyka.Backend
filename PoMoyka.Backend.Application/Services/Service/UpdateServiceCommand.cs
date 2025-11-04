using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.Application.Services.Service
{
    public class UpdateServiceCommand : IRequest
    {
        public Guid Id { get; }
        public ServiceUpdateDto Update { get; }

        public UpdateServiceCommand(Guid id, ServiceUpdateDto update)
        {
            Id = id;
            Update = update;
        }
    }

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IApplicationDbContext _context;
        
        public UpdateServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FindAsync(request.Id);

            if (service == null) 
            {
                throw new Exception($"Service with Id '{request.Id}' was not found");    
            }

            var nameExists = await _context.Services
                .AnyAsync(s => s.Name == request.Update.Name && s.Id != request.Id, cancellationToken);
            if (nameExists) 
            {
                throw new InvalidOperationException($"Service with name '{request.Update.Name}' already exists");
            }

            service.Name = request.Update.Name;
            service.Description = request.Update.Description;
            service.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
