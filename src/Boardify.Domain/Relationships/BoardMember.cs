using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class BoardMember
    {
        public int UserId { get; set; }
        public int BoardId { get; set; }

        public User User { get; set; } = null!;
        public Board Board { get; set; } = null!;
    }
}