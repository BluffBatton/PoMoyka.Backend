using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class BookingEntityConfiguration : BaseEntityConfiguration<Booking>
    {
        public override void Configure(EntityTypeBuilder<Booking> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.BookedTime)
                .IsRequired();
            builder.Property(b => b.Status)
                .IsRequired();
            builder.HasOne(b => b.CenterService)
                .WithMany(cs => cs.Bookings)
                .HasForeignKey(b => b.CenterServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
