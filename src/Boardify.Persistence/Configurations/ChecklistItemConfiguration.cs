using Boardify.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class ChecklistItemConfiguration : IEntityTypeConfiguration<ChecklistItem>
    {
        public void Configure(EntityTypeBuilder<ChecklistItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Name).IsRequired().HasMaxLength(255);
            builder.Property(ci => ci.IsChecked).IsRequired();
            builder.Property(ci => ci.Position).IsRequired();

            builder.HasOne(ci => ci.Card)
                   .WithMany(c => c.ChecklistItem)
                   .HasForeignKey(ci => ci.CardId);
        }
    }
}