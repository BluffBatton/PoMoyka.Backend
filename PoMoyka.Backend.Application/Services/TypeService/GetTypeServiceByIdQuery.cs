using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.TypeService
{
    public class GetTypeServiceByIdQuery : IRequest<TypeServiceDto>
    {
        public Guid Id { get; set; }

        public GetTypeServiceByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetTypeServiceByIdQueryHandler : IRequestHandler<GetTypeServiceByIdQuery, TypeServiceDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTypeServiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TypeServiceDto> Handle(GetTypeServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var typeService = await _context.TypeServices
                .AsNoTracking()
                .Include(ts => ts.Service)
                .FirstOrDefaultAsync(ts => ts.Id == request.Id, cancellationToken);

            if (typeService == null)
            {
                throw new Exception($"TypeService with Id '{request.Id}' was not found");
            }

            return new TypeServiceDto
            {
                Id = typeService.Id,
                ServiceId = typeService.ServiceId,
                ServiceName = typeService.Service != null ? typeService.Service.Name : "",
                CarType = (Contracts.DTOs.Enums.CarType)typeService.CarType
            };
        }
    }
}

