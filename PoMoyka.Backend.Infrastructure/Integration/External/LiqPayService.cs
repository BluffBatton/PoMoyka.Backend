using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PoMoyka.Backend.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PoMoyka.Backend.Infrastructure.Integration.External
{
    public class LiqPayService : ILiqPayService
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly LiqPayClient _liqPayClient;
        private readonly ILogger<LiqPayService> _logger;

        public LiqPayService(IConfiguration configuration, ILogger<LiqPayService> logger)
        {
            _logger = logger;
            _publicKey = configuration["LiqPay:PublicKey"]!;
            _privateKey = configuration["LiqPay:PrivateKey"]!;
            _liqPayClient = new LiqPayClient(_publicKey, _privateKey);

            _logger.LogInformation("LiqPayService initialized:");
            _logger.LogInformation("PublicKey = {Key}", _publicKey);
            _logger.LogInformation("PrivateKey = {Key}", _privateKey[..10] + "********");
        }
        public (string Data, string Signature) GeneratePaymentData(Guid orderId, decimal amount, string description)
        {
            var payload = new
            {
                public_key = _publicKey,
                version = "3",
                action = "pay",
                amount = amount,
                currency = "UAH",
                description = description,
                order_id = orderId.ToString(),
                language = "en",
                result_url = "https://pomoyka.app/payment-success", // redirect after payment(редірект після оплати
                server_url = "https://pomoyka-backend.onrender.com/api/Booking/payment-callback" // callback LiqPay
            };

            var json = JsonConvert.SerializeObject(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            using var sha1 = SHA1.Create();
            var signatureSource = _privateKey + data + _privateKey;
            var signatureBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(signatureSource));
            var signature = Convert.ToBase64String(signatureBytes);

            return (data, signature);
        }

        public bool VerifyCallback(string data, string signature)
        {
            try
            {
                using var sha1 = SHA1.Create();
                var signatureSource = _privateKey + data + _privateKey;
                var signatureBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(signatureSource));
                var expectedSignature = Convert.ToBase64String(signatureBytes);

                return signature == expectedSignature;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying LiqPay callback signature");
                return false;
            }
        }

        public LiqPayCallbackData ParseCallbackData(string data)
        {
            try
            {
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(data));
                dynamic? callbackData = JsonConvert.DeserializeObject(json);

                if (callbackData == null)
                {
                    throw new Exception("Failed to deserialize callback data");
                }

                return new LiqPayCallbackData
                {
                    BookingId = Guid.Parse((string)callbackData.order_id),
                    Status = (string)callbackData.status, // "success", "failure", "error", "pending"
                    Amount = (decimal)callbackData.amount,
                    TransactionId = callbackData.transaction_id != null ? (string)callbackData.transaction_id : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing LiqPay callback data");
                throw;
            }
        }
    }
}