using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Data.Common;

namespace PoMoyka.Backend.Infrastructure.Data.Configuration
{
    internal class TypeServiceEntityConfiguration : BaseEntityConfiguration<TypeService>
    {
        public override void Configure(EntityTypeBuilder<TypeService> builder)
        {
            base.Configure(builder);

            builder.Property(ts => ts.CarType)
                .IsRequired();
            builder.HasOne(ts => ts.Service)
                .WithMany(s => s.TypeServices)
                .HasForeignKey(ts => ts.ServiceID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
