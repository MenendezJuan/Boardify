namespace Boardify.Application.Features.CardLabels.Models
{
    public class UpdateCardLabelModel
    {
        public int CardId { get; set; }
        public int LabelId { get; set; }
        public int NewLabelId { get; set; }
    }
}