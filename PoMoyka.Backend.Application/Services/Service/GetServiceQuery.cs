using AutoMapper;
using MediatR;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Service
{
    public class GetServiceQuery : IRequest<ServiceDto>{}

    public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, ServiceDto>
    {
        IApplicationDbContext _context;
        IMapper _mapper;

        public GetServiceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
