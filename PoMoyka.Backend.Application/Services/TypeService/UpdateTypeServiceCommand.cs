using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.Application.Services.TypeService
{
    public class UpdateTypeServiceCommand : IRequest
    {
        public Guid Id { get; }
        public TypeServiceUpdateDto Update { get; }

        public UpdateTypeServiceCommand(Guid id, TypeServiceUpdateDto update)
        {
            Id = id;
            Update = update;
        }
    }

    public class UpdateTypeServiceCommandHandler : IRequestHandler<UpdateTypeServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTypeServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTypeServiceCommand request, CancellationToken cancellationToken)
        {
            var typeService = await _context.TypeServices
                .FirstOrDefaultAsync(ts => ts.Id == request.Id, cancellationToken);

            if (typeService == null)
            {
                throw new Exception($"TypeService with Id '{request.Id}' was not found");
            }

            // Check if service exists
            var serviceExists = await _context.Services
                .AnyAsync(s => s.Id == request.Update.ServiceId, cancellationToken);

            if (!serviceExists)
            {
                throw new Exception($"Service with Id '{request.Update.ServiceId}' not found");
            }

            // Check if this combination already exists (excluding current record)
            var domainCarType = (Domain.Enums.CarType)request.Update.CarType;
            var exists = await _context.TypeServices
                .AnyAsync(ts => ts.ServiceId == request.Update.ServiceId 
                    && ts.CarType == domainCarType
                    && ts.Id != request.Id, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"TypeService for Service '{request.Update.ServiceId}' and CarType '{request.Update.CarType}' already exists");
            }

            typeService.ServiceId = request.Update.ServiceId;
            typeService.CarType = domainCarType;
            typeService.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

