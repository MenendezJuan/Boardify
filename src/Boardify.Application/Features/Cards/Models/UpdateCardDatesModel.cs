namespace Boardify.Application.Features.Cards.Models
{
    public class UpdateCardDatesModel
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
