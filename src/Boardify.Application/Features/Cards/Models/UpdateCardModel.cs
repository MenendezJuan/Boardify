namespace Boardify.Application.Features.Cards.Models
{
    public class UpdateCardModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ColumnId { get; set; }
    }
}