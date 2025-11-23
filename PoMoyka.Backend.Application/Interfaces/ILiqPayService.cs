namespace PoMoyka.Backend.Application.Interfaces
{
    public interface ILiqPayService
    {
        (string Data, string Signature) GeneratePaymentData(
            Guid orderId,
            decimal amount,
            string description
        );

        bool VerifyCallback(string data, string signature);

        LiqPayCallbackData ParseCallbackData(string data);
    }

    public class LiqPayCallbackData
    {
        public Guid BookingId { get; set; }
        public string Status { get; set; } // "success", "failure", "error", "pending"
        public decimal Amount { get; set; }
        public string? TransactionId { get; set; }
    }
}
