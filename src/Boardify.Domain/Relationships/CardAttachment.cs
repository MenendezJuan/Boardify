using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class CardAttachment
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}