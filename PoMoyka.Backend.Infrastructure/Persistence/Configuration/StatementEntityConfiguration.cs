using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Infrastructure.Persistence.Common;

namespace PoMoyka.Backend.Infrastructure.Persistence.Configuration
{
    internal class StatementEntityConfiguration : BaseEntityConfiguration<Statement>
    {
        public override void Configure(EntityTypeBuilder<Statement> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Topic)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(s => s.Message)
                .IsRequired()
                .HasMaxLength(2000);
            builder.Property(s => s.Status)
                .IsRequired();
        }
    }
}
