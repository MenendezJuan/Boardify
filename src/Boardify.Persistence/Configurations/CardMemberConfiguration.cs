using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class CardMemberConfiguration : EntityTypeConfigurationBase<CardMember>
    {
        public override void Configure(EntityTypeBuilder<CardMember> builder)
        {
            builder.HasKey(cm => new { cm.UserId, cm.CardId });

            builder.HasOne(cm => cm.User)
                   .WithMany(u => u.CardMembers)
                   .HasForeignKey(cm => cm.UserId);

            builder.HasOne(cm => cm.Card)
                   .WithMany(c => c.CardMembers)
                   .HasForeignKey(cm => cm.CardId);
        }
    }
}