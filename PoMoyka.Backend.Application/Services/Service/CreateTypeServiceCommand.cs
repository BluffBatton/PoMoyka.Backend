using AutoMapper;
using MediatR;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Service
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
            var newTypeService = _mapper.Map<TypeService>(request.Dto);
            newTypeService.CreatedAt = DateTime.UtcNow;

            await _context.TypeServices.AddAsync(newTypeService, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newTypeService.Id;
        }
    }
}
