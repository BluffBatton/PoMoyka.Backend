using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Service
{
    public class GetServiceByIdQuery : IRequest<ServiceDto>
    {
        public Guid Id { get; set; }
        public GetServiceByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if(service == null)
            {
                throw new Exception($"Service with Id '{request.Id}' was not found");
            }

            return _mapper.Map<ServiceDto>(service);
        }
    }
}
