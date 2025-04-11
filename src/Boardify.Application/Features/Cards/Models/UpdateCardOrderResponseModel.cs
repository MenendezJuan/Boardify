namespace Boardify.Application.Features.Cards.Models
{
    public class UpdateCardOrderResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int ColumnId { get; set; }
    }
}