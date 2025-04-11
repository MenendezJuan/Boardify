using Boardify.Domain.BaseEntities;
using Boardify.Domain.Relationships;

namespace Boardify.Domain.Entities
{
    public class Workspace : BaseEntity
    {
        public string? Name { get; set; }
        public List<Board> Boards { get; set; } = [];
        public List<UserWorkspace> UserWorkspaces { get; set; } = [];
    }
}