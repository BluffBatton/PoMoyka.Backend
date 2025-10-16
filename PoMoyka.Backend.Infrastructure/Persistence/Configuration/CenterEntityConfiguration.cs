using PoMoyka.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class CenterEntityConfiguration : BaseEntityConfiguration<Center>
    {
        public override void Configure(EntityTypeBuilder<Center> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.Latitude)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Longitude)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(c => c.User)
                .WithOne(u => u.Center)
                .HasForeignKey<Center>(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.CenterServices)
                .WithOne(cs => cs.Center)
                .HasForeignKey(cs => cs.CenterID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
