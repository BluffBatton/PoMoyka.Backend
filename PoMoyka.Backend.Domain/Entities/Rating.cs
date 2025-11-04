using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public RatingNumber RatingNumber { get; set; }
        public Guid TransactionId { get; set; }
        public required virtual Transaction Transaction { get; set; }
    }
}
