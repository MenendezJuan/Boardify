using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class CardMember
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}