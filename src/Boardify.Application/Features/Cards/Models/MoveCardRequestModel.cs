namespace Boardify.Application.Features.Cards.Models
{
    public class MoveCardRequestModel
    {
        public int CardId { get; set; }
        public int SourceColumnId { get; set; }
        public int DestinationColumnId { get; set; }
        public List<int> SourceCardOrder { get; set; } = new List<int>();
        public List<int> DestinationCardOrder { get; set; } = new List<int>();
    }
}