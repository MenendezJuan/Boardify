namespace Boardify.Application.Features.Cards.Models
{
    public class RemoveMemberRequestModel
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
    }
}