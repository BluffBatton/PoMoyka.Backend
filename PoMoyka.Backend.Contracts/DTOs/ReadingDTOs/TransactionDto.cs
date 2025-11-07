namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid BookingId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
