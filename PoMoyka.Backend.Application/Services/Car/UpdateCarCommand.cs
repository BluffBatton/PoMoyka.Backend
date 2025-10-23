using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.Application.Services.Car
{
    public class UpdateCarCommand : IRequest
    {
        public CarUpdateDto Car { get; }
        public UpdateCarCommand(CarUpdateDto car)
        {
            Car = car;
        }
    }

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;

        public UpdateCarCommandHandler(
            IApplicationDbContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if(userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.UserId == userId.Value, cancellationToken);
            if(car == null)
            {
                throw new Exception($"Server error: Car for user with id {userId} not found in db. Please contact admin");
            }
            var dto = request.Car;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                car.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.LicensePlate))
            {
                car.LicensePlate = dto.LicensePlate;
            }
            if (dto.CarType.HasValue)
            {
                car.CarType = (Domain.Enums.CarType)dto.CarType.Value;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
