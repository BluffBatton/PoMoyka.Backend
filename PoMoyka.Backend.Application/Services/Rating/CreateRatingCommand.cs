using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Services.Rating
{
    public class CreateRatingCommand : IRequest<Guid>
    {
        public RatingCreateDto RatingCreateDto { get; }

        public CreateRatingCommand(RatingCreateDto ratingCreateDto)
        {
            RatingCreateDto = ratingCreateDto;
        }
    }

    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContextService _userContextService;

        public CreateRatingCommandHandler(
            IApplicationDbContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<Guid> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (!userId.HasValue || userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var dto = request.RatingCreateDto;

            // Получаем транзакцию с бронированием
            var transaction = await _context.Transactions
                .Include(t => t.Booking)
                    .ThenInclude(b => b.User)
                .Include(t => t.Rating)
                .FirstOrDefaultAsync(t => t.Id == dto.TransactionId, cancellationToken);

            if (transaction == null)
            {
                throw new Exception($"Transaction with ID {dto.TransactionId} not found");
            }

            // Проверка: транзакция принадлежит текущему пользователю
            if (transaction.Booking.UserId != userId.Value)
            {
                throw new UnauthorizedAccessException("You can only rate your own transactions");
            }

            // Проверка: бронирование должно быть завершено (Done)
            if (transaction.Booking.Status != BookingStatus.Done)
            {
                throw new Exception("You can only rate completed bookings");
            }

            // Проверка: рейтинг уже существует
            if (transaction.Rating != null)
            {
                throw new Exception("Rating for this transaction already exists. Use update endpoint to change it.");
            }

            // Создаем новый рейтинг
            var rating = new Domain.Entities.Rating
            {
                RatingNumber = (Domain.Enums.RatingNumber)(int)dto.RatingValue,
                TransactionId = dto.TransactionId,
                Transaction = transaction,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Ratings.AddAsync(rating, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return rating.Id;
        }
    }
}

