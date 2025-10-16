using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class ServiceEntityConfiguration : BaseEntityConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            base.Configure(builder);

            builder. Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Description)
                .HasMaxLength(500);
            builder.HasMany(s => s.TypeServices)
                .WithOne(ts => ts.Service)
                .HasForeignKey(ts => ts.ServiceID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
