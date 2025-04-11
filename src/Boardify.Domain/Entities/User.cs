using Boardify.Domain.BaseEntities;
using Boardify.Domain.Relationships;

namespace Boardify.Domain.Entities
{
    public class User : BaseEntity
    {
        public bool IsAdmin { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? Salt { get; set; }
        public List<UserWorkspace> UserWorkspaces { get; set; } = new List<UserWorkspace>();
        public List<BoardMember> Boards { get; set; } = new List<BoardMember>();
        public List<CardMember> CardMembers { get; set; } = new List<CardMember>();
        public List<CardActivity> CardActivities { get; set; } = new List<CardActivity>();
        public List<CardComment> CardComments { get; set; } = new List<CardComment>();
    }
}