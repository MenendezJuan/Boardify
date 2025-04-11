using Boardify.Domain.Entities;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class UserConfiguration : EntityTypeConfigurationBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(30);
            builder.Property(u => u.LastName).HasMaxLength(45);
            builder.Property(u => u.Email).HasMaxLength(120);

            builder.Property(u => u.IsAdmin)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.Avatar)
                .HasMaxLength(255)
                .IsRequired(false);
        }
    }
}