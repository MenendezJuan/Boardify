using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class CardLabel
    {
        public int CardId { get; set; }
        public int LabelId { get; set; }
        public BoardLabel BoardLabel { get; set; } = null!;
        public Card Card { get; set; } = null!;
    }
}