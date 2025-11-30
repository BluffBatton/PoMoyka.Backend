using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class RatingCreateDto
    {
        public Guid TransactionId { get; set; }
        public RatingNumber RatingValue { get; set; }
    }
}

