using Domain.Enums;

namespace Domain.Entities
{
    internal class Rating
    {
        public Guid RatingID { get; set; }
        public Transaction Transaction { get; set; }
        public RatingNumber RatingNumber { get; set; }
    }
}
