using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class CenterServiceEntityConfiguration : BaseEntityConfiguration<CenterService>
    {
        public override void Configure(EntityTypeBuilder<CenterService> builder)
        {
            base.Configure(builder);

            builder.Property(cs => cs.Price)
                .IsRequired();
            builder.HasOne(cs => cs.Center)
                .WithMany(c => c.CenterServices)
                .HasForeignKey(cs => cs.CenterId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(cs => cs.TypeService)
                .WithMany(ts => ts.CenterServices)
                .HasForeignKey(cs => cs.TypeServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
