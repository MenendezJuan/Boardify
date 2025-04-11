using Boardify.Domain.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class CardAttachmentConfiguration : IEntityTypeConfiguration<CardAttachment>
    {
        public void Configure(EntityTypeBuilder<CardAttachment> builder)
        {
            builder.HasKey(ca => ca.Id);

            builder.Property(ca => ca.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(ca => ca.FilePath)
                   .IsRequired();

            builder.Property(ca => ca.FileSize)
                   .IsRequired();

            builder.Property(ca => ca.ContentType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ca => ca.CreatedDate)
                   .IsRequired();

            builder.HasOne(ca => ca.Card)
                   .WithMany(c => c.CardAttachments)
                   .HasForeignKey(ca => ca.CardId);
        }
    }
}