using Boardify.Domain.Entities;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class ColumnConfiguration : EntityTypeConfigurationBase<Column>
    {
        public override void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(90);
            builder.Property(c => c.Position)
               .IsRequired()
               .HasColumnType("int");

            builder.HasOne(c => c.Board)
                .WithMany(b => b.Columns)
                .HasForeignKey(c => c.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Cards)
                .WithOne(card => card.Column)
                .HasForeignKey(card => card.ColumnId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}