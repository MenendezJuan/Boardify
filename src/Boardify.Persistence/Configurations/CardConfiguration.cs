using Boardify.Domain.Entities;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class CardConfiguration : EntityTypeConfigurationBase<Card>
    {
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(c => c.DueDate)
                .IsRequired(false);
            builder.Property(c => c.StartDate)
                .IsRequired(false);
            builder.Property(c => c.ColumnId)
                .IsRequired();

            builder.HasOne(c => c.Column)
                .WithMany(col => col.Cards)
                .HasForeignKey(c => c.ColumnId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CardAttachments)
             .WithOne(a => a.Card)
             .HasForeignKey(a => a.CardId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Reporter)
                    .WithMany()
                    .HasForeignKey(c => c.ReporterId)
                    .IsRequired(false);
        }
    }
}