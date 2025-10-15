using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Data.Common;

namespace PoMoyka.Backend.Infrastructure.Data.Configuration 
{
    internal class RatingEntityConfiguration : BaseEntityConfiguration<Rating>
    {
        public override void Configure(EntityTypeBuilder<Rating> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.RatingNumber)
                .IsRequired();
            builder.HasOne(r => r.Transaction)
                .WithOne(t => t.Rating)
                .HasForeignKey<Rating>(r => r.TransactionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
