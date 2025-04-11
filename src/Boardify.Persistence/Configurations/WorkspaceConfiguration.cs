using Boardify.Domain.Entities;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class WorkspaceConfiguration : EntityTypeConfigurationBase<Workspace>
    {
        public override void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(72);
        }
    }
}