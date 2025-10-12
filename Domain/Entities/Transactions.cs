namespace Domain.Entities
{
    internal class Transaction
    {
        public Guid TransactionID { get; set; }
        Booking Booking { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
