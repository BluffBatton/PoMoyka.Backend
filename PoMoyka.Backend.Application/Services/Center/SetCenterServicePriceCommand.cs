using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Services.Center
{
    public class SetCenterServicePriceCommand : IRequest<Guid>
    {
        public CenterServicePriceDto Dto { get; }
        public SetCenterServicePriceCommand(CenterServicePriceDto dto)
        {
            Dto = dto;
        }
    }

    public class SetCenterServicePriceCommandHandler : IRequestHandler<SetCenterServicePriceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SetCenterServicePriceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(SetCenterServicePriceCommand request, CancellationToken cancellationToken)
        {
            //var existingPrice = await _context.CenterServices
            //    .FirstOrDefaultAsync(cs =>
            //    cs.CenterId == request.Dto.CenterId &&
            //    cs.TypeServiceId == request.Dto.TypeServiceId,
            //    cancellationToken);

            //if (existingPrice == null) 
            //{
            //    existingPrice.Price = request.Dto.Price;
            //    await _context.SaveChangesAsync(cancellationToken);
            //    return existingPrice.Id;
            //}

            var newPriceEntry = _mapper.Map<CenterService>(request.Dto);
            newPriceEntry.CreatedAt = DateTime.UtcNow;

            await _context.CenterServices.AddAsync(newPriceEntry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newPriceEntry.Id;
        }
    }
}
