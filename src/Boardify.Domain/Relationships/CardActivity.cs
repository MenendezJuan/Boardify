using Boardify.Domain.BaseEntities;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;

namespace Boardify.Domain.Relationships
{
    public class CardActivity : BaseEntity
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;
        public User User { get; set; } = null!;
        public string? Activity { get; set; }
        public CardEventTypeEnum EventType { get; set; }
    }
}
