using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class UserWorkspaceConfiguration : EntityTypeConfigurationBase<UserWorkspace>
    {
        public override void Configure(EntityTypeBuilder<UserWorkspace> builder)
        {
            builder.HasKey(uw => new { uw.UserId, uw.WorkspaceId });

            builder.Property(uw => uw.IsOwner)
                 .IsRequired();

            builder.HasOne(uw => uw.User)
                .WithMany(u => u.UserWorkspaces)
                .HasForeignKey(uw => uw.UserId);

            builder.HasOne(uw => uw.Workspace)
                .WithMany(w => w.UserWorkspaces)
                .HasForeignKey(uw => uw.WorkspaceId);
        }
    }
}