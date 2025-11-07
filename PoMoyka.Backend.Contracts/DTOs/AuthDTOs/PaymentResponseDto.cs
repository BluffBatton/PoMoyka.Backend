namespace PoMoyka.Backend.Contracts.DTOs.AuthDTOs
{
    public class PaymentResponseDto
    {
        public Guid BookingId { get; set; }
        public string Data { get; set; }
        public string Signature { get; set; }
    }
}
