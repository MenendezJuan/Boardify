namespace Boardify.Application.Features.Comments.Models
{
    public class AddCommentModel
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
    }
}
