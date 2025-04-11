namespace Boardify.Application.Features.ChecklistItems.Models
{
    public class CreateChecklistItemRequest
    {
        public int CardId { get; set; }
        public string? Name { get; set; }
    }
}