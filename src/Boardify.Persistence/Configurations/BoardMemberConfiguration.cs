using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boardify.Persistence.Configurations
{
    public class BoardMemberConfiguration : EntityTypeConfigurationBase<BoardMember>
    {
        public override void Configure(EntityTypeBuilder<BoardMember> builder)
        {
            builder.HasKey(bm => new { bm.UserId, bm.BoardId });

            builder.HasOne(bm => bm.User)
                   .WithMany(u => u.Boards)
                   .HasForeignKey(bm => bm.UserId);

            builder.HasOne(bm => bm.Board)
                   .WithMany(b => b.BoardMembers)
                   .HasForeignKey(bm => bm.BoardId);
        }
    }
}