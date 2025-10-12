using Domain.Enums;

namespace Domain.Entities
{
    internal class Rating
    {
        public Guid RatingID { get; set; }
        Transaction Transaction { get; set; }
        RatingNumber RatingNumber { get; set; }
    }
}
