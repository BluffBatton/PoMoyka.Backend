using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.AuthDTOs;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class CreateBookingCommand : IRequest<PaymentResponseDto>
    {
        public BookingCreateDto CreateBookingDto { get; set; }
        public CreateBookingCommand(BookingCreateDto createBookingDto)
        {
            CreateBookingDto = createBookingDto;
        }
    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, PaymentResponseDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly ILiqPayService _liqPayService;
        public CreateBookingCommandHandler(IApplicationDbContext context, IUserContextService service, ILiqPayService liqPayService)
        {
            _context = context;
            _userContextService = service;
            _liqPayService = liqPayService;
        }

        public async Task<PaymentResponseDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (!userId.HasValue || userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var dto = request.CreateBookingDto;

            // Валидация времени бронирования
            if (dto.BookedTime < DateTime.UtcNow)
            {
                throw new Exception("Booked time cannot be in the past");
            }

            // Проверка существования услуги
            var centerService = await _context.CenterServices
                .AsNoTracking()
                .Include(cs => cs.TypeService)
                    .ThenInclude(ts => ts.Service)
                .Include(cs => cs.Center)
                .FirstOrDefaultAsync(cs => cs.Id == dto.CenterServiceId, cancellationToken);

            if (centerService == null)
            {
                throw new Exception($"CenterService with ID {dto.CenterServiceId} not found");
            }

            var fromTime = dto.BookedTime.AddMinutes(-10);
            var toTime = dto.BookedTime.AddMinutes(20);

            var conflictingBooking = await _context.Bookings
                .AnyAsync(b =>
                    b.CenterServiceId == dto.CenterServiceId &&
                    b.BookedTime >= fromTime &&
                    b.BookedTime <= toTime &&
                    b.Status == BookingStatus.Done,
                    cancellationToken);

            if (conflictingBooking)
            {
                throw new Exception("This time slot is already booked");
            }

            var booking = new Domain.Entities.Booking
            {
                CenterServiceId = dto.CenterServiceId,
                BookedTime = dto.BookedTime,
                UserId = userId.Value,
                Status = BookingStatus.Waiting, // Ожидание оплаты
                CreatedAt = DateTime.UtcNow
            };

            string description = $"Service '{centerService.TypeService.Service.Name}' at {centerService.Center.Name} on {dto.BookedTime:dd.MM.yyyy HH:mm}";

            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            (string data, string signature) = _liqPayService.GeneratePaymentData(
                booking.Id,
                centerService.Price,
                description
            );

            return new PaymentResponseDto
            {
                BookingId = booking.Id,
                Data = data,
                Signature = signature
            };
        }
    }
}