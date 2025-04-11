using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class CardActivityConfiguration : EntityTypeConfigurationBase<CardActivity>
    {
        public override void Configure(EntityTypeBuilder<CardActivity> builder)
        {
            builder.HasKey(ca => ca.Id);

            builder.Property(ca => ca.Activity)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ca => ca.CreationTime)
                .IsRequired();

            builder.Property(ca => ca.LastModifiedTime)
                .IsRequired();

            builder.Property(ca => ca.Status)
                .IsRequired();

            builder.Property(ca => ca.EventType)
                .IsRequired();

            builder.HasOne(ca => ca.Card)
               .WithMany(c => c.CardActivities)
               .HasForeignKey(ca => ca.CardId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.User)
               .WithMany(u => u.CardActivities)
               .HasForeignKey(ca => ca.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
