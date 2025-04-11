using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class UserWorkspace
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public bool IsOwner { get; set; }
    }
}