using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string? Name { get; set; }
        public bool IsChecked { get; set; }
        public int Position { get; set; }
        public Card Card { get; set; } = null!;
        public List<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
    }
}