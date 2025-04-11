using Boardify.Domain.Entities;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class BoardConfiguration : EntityTypeConfigurationBase<Board>
    {
        public override void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Description).HasMaxLength(255).IsRequired(false);
            builder.Property(b => b.Visibility).IsRequired();

            builder.HasOne(b => b.Workspace)
                   .WithMany(w => w.Boards)
                   .HasForeignKey(b => b.WorkspaceId);
        }
    }
}