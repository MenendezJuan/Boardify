namespace Boardify.Application.Features.Cards.Models
{
    public class CardMemberResponseModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}