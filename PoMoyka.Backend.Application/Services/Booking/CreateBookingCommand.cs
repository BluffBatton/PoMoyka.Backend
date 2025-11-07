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
            var dto = request.CreateBookingDto;

            var centerService = await _context.CenterServices
                .AsNoTracking()
                .Include(cs => cs.TypeService.Service)
                .FirstOrDefaultAsync(cs => cs.Id == dto.CenterServiceId, cancellationToken);

            var booking = new Domain.Entities.Booking
            {
                CenterServiceId = dto.CenterServiceId,
                BookedTime = dto.BookedTime,
                UserId = userId.Value,
                Status = BookingStatus.Done
            };

            booking.CreatedAt = DateTime.UtcNow;

            string description = $"Payed services '{centerService.TypeService.Service.Name}' on {dto.BookedTime.ToShortDateString()}";

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