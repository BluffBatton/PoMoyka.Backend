using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.Application.Services.TypeService
{
    public class CreateTypeServiceCommand : IRequest<Guid>
    {
        public TypeServiceCreateDto Dto { get; }

        public CreateTypeServiceCommand(TypeServiceCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateTypeServiceCommandHandler : IRequestHandler<CreateTypeServiceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTypeServiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTypeServiceCommand request, CancellationToken cancellationToken)
        {
            // 1. Перевіряємо, що Service існує
            var serviceExists = await _context.Services
                .AnyAsync(s => s.Id == request.Dto.ServiceId, cancellationToken);

            if (!serviceExists)
            {
                throw new Exception($"Service with Id '{request.Dto.ServiceId}' not found");
            }

            // 2. Перевіряємо, що немає дубля (ServiceId + CarType)
            var domainCarType = (Domain.Enums.CarType)request.Dto.CarType;

            var exists = await _context.TypeServices
                .AnyAsync(ts =>
                    ts.ServiceId == request.Dto.ServiceId &&
                    ts.CarType == domainCarType,
                    cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"TypeService for Service '{request.Dto.ServiceId}' and CarType '{request.Dto.CarType}' already exists");
            }

            // 3. Створюємо новий запис
            var newTypeService = _mapper.Map<Domain.Entities.TypeService>(request.Dto);
            newTypeService.CreatedAt = DateTime.UtcNow;

            await _context.TypeServices.AddAsync(newTypeService, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newTypeService.Id;
        }
    }
}
