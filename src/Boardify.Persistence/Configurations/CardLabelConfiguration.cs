using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class CardLabelConfiguration : EntityTypeConfigurationBase<CardLabel>
    {
        public override void Configure(EntityTypeBuilder<CardLabel> builder)
        {
            builder.HasKey(cl => new { cl.CardId, cl.LabelId });

            builder.HasOne(cl => cl.Card)
                .WithMany(c => c.CardLabels)
                .HasForeignKey(cl => cl.CardId);

            builder.HasOne(cl => cl.BoardLabel)
                .WithMany(bl => bl.CardLabels)
                .HasForeignKey(cl => cl.LabelId);
        }
    }
}