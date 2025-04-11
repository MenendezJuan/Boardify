namespace Boardify.Application.Features.Comments.Models
{
    public class AddCommentResponseModel
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
