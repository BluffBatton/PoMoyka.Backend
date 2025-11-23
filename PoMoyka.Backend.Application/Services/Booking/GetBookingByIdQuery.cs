using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Services.Booking
{
    public class GetBookingByIdQuery : IRequest<BookingDetailedDto>
    {
        public Guid Id { get; }

        public GetBookingByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDetailedDto>
    {
        private readonly IApplicationDbContext _context;

        public GetBookingByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDetailedDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.CenterService)
                    .ThenInclude(cs => cs.Center)
                .Include(b => b.CenterService)
                    .ThenInclude(cs => cs.TypeService)
                        .ThenInclude(ts => ts.Service)
                .Include(b => b.Transaction)
                .Where(b => b.Id == request.Id)
                .Select(b => new BookingDetailedDto
                {
                    Id = b.Id,
                    BookedTime = b.BookedTime,
                    Status = (BookingStatus)b.Status,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    UserId = b.UserId,
                    UserFirstName = b.User.FirstName,
                    UserLastName = b.User.LastName,
                    UserEmail = b.User.Email,
                    CenterId = b.CenterService.CenterId,
                    CenterName = b.CenterService.Center.Name,
                    CenterAddress = b.CenterService.Center.Address,
                    CenterServiceId = b.CenterServiceId,
                    ServiceName = b.CenterService.TypeService.Service.Name,
                    ServiceDescription = b.CenterService.TypeService.Service.Description,
                    CarType = (CarType)b.CenterService.TypeService.CarType,
                    Price = b.CenterService.Price,
                    TransactionId = b.Transaction != null ? b.Transaction.Id : null,
                    TransactionAmount = b.Transaction != null ? b.Transaction.Amount : null
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (booking == null)
            {
                throw new Exception($"Booking with ID {request.Id} not found");
            }

            return booking;
        }
    }
}

