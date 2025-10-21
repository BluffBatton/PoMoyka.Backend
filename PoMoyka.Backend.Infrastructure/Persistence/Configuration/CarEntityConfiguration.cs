using PoMoyka.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class CarEntityConfiguration : BaseEntityConfiguration<Car>
    {
        public override void Configure(EntityTypeBuilder<Car> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(10);
            builder.HasIndex(c => c.LicensePlate)
                .IsUnique();
            builder.Property (c => c.CarType)
                .IsRequired();
            builder.HasOne(c => c.User)
                .WithOne(u => u.Car)
                .HasForeignKey<Car>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
