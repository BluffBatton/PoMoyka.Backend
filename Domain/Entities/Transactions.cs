namespace Domain.Entities
{
    internal class Transaction
    {
        public Guid TransactionID { get; set; }
        public Guid BookingID { get; set; }
        public Booking Booking { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid RatingID { get; set; }
        public Rating Rating { get; set; }
    }
}
