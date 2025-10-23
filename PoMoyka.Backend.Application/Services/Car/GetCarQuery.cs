using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Car
{
    public class GetCarQuery : IRequest<CarDto>{}

    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, CarDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public GetCarQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<CarDto> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if(userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            var car = await _context.Cars.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId.Value, cancellationToken);
            if(car == null)
            {
                throw new Exception($"Server error: Car for user with id {userId} not found in db. Please contact admin");
            }

            return _mapper.Map<CarDto>(car);
        }
    }
}
