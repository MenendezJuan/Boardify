using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class BoardLabelConfiguration : EntityTypeConfigurationBase<BoardLabel>
    {
        public override void Configure(EntityTypeBuilder<BoardLabel> builder)
        {
            builder.HasKey(bm => new { bm.Id });

            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Colour).HasMaxLength(100).IsRequired();

            builder.HasOne(bm => bm.Board)
                   .WithMany(b => b.BoardLabels)
                   .HasForeignKey(bm => bm.BoardId);
        }
    }
}