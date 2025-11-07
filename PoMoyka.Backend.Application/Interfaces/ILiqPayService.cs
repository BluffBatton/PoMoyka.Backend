namespace PoMoyka.Backend.Application.Interfaces
{
    public interface ILiqPayService
    {
        (string Data, string Signature) GeneratePaymentData(
            Guid orderId,
            decimal amount,
            string description
        );
    }
}
