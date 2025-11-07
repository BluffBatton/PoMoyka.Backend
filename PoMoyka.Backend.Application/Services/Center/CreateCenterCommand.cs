using AutoMapper;
using MediatR;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class CreateCenterCommand : IRequest<Guid>
    {
        public СenterCreateDto Dto { get; }
        public CreateCenterCommand(СenterCreateDto dto) 
        {
            Dto = dto;
        }
    }

    public class CreateCenterCommandHandler : IRequestHandler<CreateCenterCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateCenterCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCenterCommand request, CancellationToken cancellationToken)
        {
            var newCenter = _mapper.Map<Domain.Entities.Center>(request.Dto);
            newCenter.CreatedAt = DateTime.UtcNow;

            await _context.Centers.AddAsync(newCenter, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newCenter.Id;
        }
    }
}
