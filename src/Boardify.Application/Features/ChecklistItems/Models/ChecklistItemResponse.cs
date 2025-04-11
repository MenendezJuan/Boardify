namespace Boardify.Application.Features.ChecklistItems.Models
{
    public class ChecklistItemResponse
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string? Name { get; set; }
        public bool IsChecked { get; set; }
        public int Position { get; set; }
    }
}