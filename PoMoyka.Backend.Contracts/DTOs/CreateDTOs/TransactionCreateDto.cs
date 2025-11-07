namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class TransactionCreateDto
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
    }
}
