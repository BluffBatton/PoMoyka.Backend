using AutoMapper;
using LiqPay.SDK;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Enums;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PoMoyka.Backend.Application.Services.Payment
{
    public class ConfirmPaymentCommand : IRequest
    {
        public string Data { get; }
        public string Signature { get; }

        public ConfirmPaymentCommand(string data, string signature)
        {
            Data = data;
            Signature = signature;
        }
    }

    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly string _privateKey;
        private readonly IMapper _mapper;

        public ConfirmPaymentCommandHandler(IApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _privateKey = configuration["LiqPay:PrivateKey"]!;
            _mapper = mapper;
        }

        public async Task Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            using var sha1 = SHA1.Create();
            var signatureSource = _privateKey + request.Data + _privateKey;
            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(signatureSource));
            var expectedSignature = Convert.ToBase64String(hashBytes);

            if (expectedSignature != request.Signature)
                throw new SecurityException("LiqPay signature mismatch.");

            var jsonBytes = Convert.FromBase64String(request.Data);
            var jsonString = Encoding.UTF8.GetString(jsonBytes);
            var liqPayResponse = JsonSerializer.Deserialize<JsonElement>(jsonString);

            var orderIdStr = liqPayResponse.GetProperty("order_id").GetString();
            var status = liqPayResponse.GetProperty("status").GetString();

            if (!Guid.TryParse(orderIdStr, out var bookingId))
                throw new InvalidOperationException("Invalid OrderId in LiqPay callback.");

            if (status is "success" or "sandbox")
            {
                var transactionExists = await _context.Transactions
                    .AnyAsync(t => t.BookingId == bookingId, cancellationToken);
                if (transactionExists)
                {
                    return;
                }

                var booking = await _context.Bookings.FindAsync(bookingId);
                if (booking != null)
                {
                    booking.Status = BookingStatus.Waiting;

                    var transactionDto = new TransactionCreateDto
                    {
                        BookingId = bookingId,
                        Amount = (decimal)liqPayResponse.GetProperty("amount").GetDouble()
                    };

                    var transaction = _mapper.Map<Domain.Entities.Transaction>(transactionDto);
                    transaction.CreatedAt = DateTime.UtcNow;

                    await _context.Transactions.AddAsync(transaction, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
