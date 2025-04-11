using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class CardComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;
        public User User { get; set; } = null!;
        public string? Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
