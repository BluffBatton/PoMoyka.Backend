using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Service
{
    public class CreateServiceCommand : IRequest<Guid>
    {
        public ServiceCreateDto service { get; set; }
    }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
    {
        public IApplicationDbContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public CreateServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var serviceExists = await _context.Services
                .AnyAsync(s => s.Name == request.service.Name, cancellationToken);

            if (serviceExists)
            {
                throw new InvalidOperationException($"Service with name '{request.service.Name}' already exists");
            }

            var service = _mapper.Map<Domain.Entities.Service>(request.service);
            service.CreatedAt = DateTime.UtcNow;

            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return service.Id;
        }
    }
}
