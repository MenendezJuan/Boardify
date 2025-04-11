namespace Boardify.Application.Features.ChecklistItems.Models
{
    public class UpdateChecklistItemResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsChecked { get; set; }
        public int Position { get; set; }
    }
}