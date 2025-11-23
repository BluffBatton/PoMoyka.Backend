using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class UpdateCenterCommand : IRequest<Unit>
    {
        public Guid Id { get; }
        public CenterUpdateDto Dto { get; }

        public UpdateCenterCommand(Guid id, CenterUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }

    public class UpdateCenterCommandHandler : IRequestHandler<UpdateCenterCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCenterCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCenterCommand request, CancellationToken cancellationToken)
        {
            var center = await _context.Centers
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (center == null)
            {
                throw new Exception($"Center with ID {request.Id} not found");
            }

            _mapper.Map(request.Dto, center);
            center.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

