using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.Application.Services.CenterService
{
    public class UpdateCenterServiceCommand : IRequest<Unit>
    {
        public Guid Id { get; }
        public CenterServiceUpdateDto Dto { get; }

        public UpdateCenterServiceCommand(Guid id, CenterServiceUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }

    public class UpdateCenterServiceCommandHandler : IRequestHandler<UpdateCenterServiceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCenterServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCenterServiceCommand request, CancellationToken cancellationToken)
        {
            var centerService = await _context.CenterServices
                .FirstOrDefaultAsync(cs => cs.Id == request.Id, cancellationToken);

            if (centerService == null)
            {
                throw new Exception($"CenterService with ID {request.Id} not found");
            }

            centerService.Price = request.Dto.Price;
            centerService.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

