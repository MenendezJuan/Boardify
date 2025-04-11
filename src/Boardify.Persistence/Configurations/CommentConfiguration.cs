using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class CommentConfiguration : EntityTypeConfigurationBase<CardComment>
    {
        public override void Configure(EntityTypeBuilder<CardComment> builder)
        {
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Comment)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(cc => cc.CreatedTime)
                .IsRequired();

            builder.HasOne(cc => cc.Card)
               .WithMany(c => c.CardComments)
               .HasForeignKey(cc => cc.CardId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cc => cc.User)
               .WithMany(c => c.CardComments)
               .HasForeignKey(cc => cc.CardId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
