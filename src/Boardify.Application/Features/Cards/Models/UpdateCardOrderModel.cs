namespace Boardify.Application.Features.Cards.Models
{
    public class UpdateCardOrderModel
    {
        public int ColumnId { get; set; }
        public List<int> CardOrder { get; set; } = new List<int>();
    }
}