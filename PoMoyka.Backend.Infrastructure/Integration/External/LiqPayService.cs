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
                server_url = "https://0e814d8cb65d.ngrok-free.app/api/Payment/callback" // callback LiqPay, need host (local host - ngrok)
            };

            var json = JsonConvert.SerializeObject(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            using var sha1 = SHA1.Create();
            var signatureSource = _privateKey + data + _privateKey;
            var signatureBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(signatureSource));
            var signature = Convert.ToBase64String(signatureBytes);

            return (data, signature);
        }
    }
}