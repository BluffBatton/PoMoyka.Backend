using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class UserImageEntityConfiguration : BaseEntityConfiguration<UserImage>
    {
        public override void Configure(EntityTypeBuilder<UserImage> builder)
        {
            base.Configure(builder);

            builder.Property(ui => ui.Name)
                .IsRequired()
                .HasMaxLength(255);
            
            builder.Property(ui => ui.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);
            
            builder.HasOne(ui => ui.User)
                .WithOne(u => u.UserImage)
                .HasForeignKey<UserImage>(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

