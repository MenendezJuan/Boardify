namespace Boardify.Application.Features.CardLabels.Models
{
    public class CardLabelModel
    {
        public int CardId { get; set; }
        public int LabelId { get; set; }
        public string? LabelName { get; set; }
        public string? LabelColour { get; set; }
    }
}