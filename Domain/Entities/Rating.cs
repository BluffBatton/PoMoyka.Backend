using Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace Domain.Entities
{
    public class Rating : BaseEntity
    {
        public RatingNumber RatingNumber { get; set; }
        public Guid TransactionID { get; set; }
        public required virtual Transaction Transaction { get; set; }
    }
}
