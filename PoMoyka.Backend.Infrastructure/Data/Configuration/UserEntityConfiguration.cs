using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Infrastructure.Data.Common;

namespace PoMoyka.Backend.Infrastructure.Data.Configuration
{
    internal class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.PasswordHash)
                .IsRequired();
            builder.Property(u => u.AvatarPath)
                .HasMaxLength(255);
            builder.Property(u => u.Role)
                .IsRequired();

            builder.HasMany(u => u.Statements)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(u => u.Car)
                .WithOne(c => c.User)
                .HasForeignKey<Car>(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Center)
                .WithOne(c => c.User)
                .HasForeignKey<Center>(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
