using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class TransactionEntityConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Amount)
                .IsRequired();
            builder.HasOne(t => t.Booking)
                .WithOne(b => b.Transaction)
                .HasForeignKey<Transaction>(t => t.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Rating)
                .WithOne(r => r.Transaction)
                .HasForeignKey<Rating>(r => r.TransactionId);
        }
    }
}
