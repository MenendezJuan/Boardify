using Boardify.Domain.BaseEntities;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Domain.Entities
{
    public class Board : BaseEntity
    {
        public int WorkspaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public VisibilityEnum Visibility { get; set; }

        public Workspace Workspace { get; set; } = null!;
        public List<BoardMember> BoardMembers { get; set; } = [];
        public List<Column> Columns { get; set; } = [];
        public List<BoardLabel> BoardLabels { get; set; } = [];
    }
}