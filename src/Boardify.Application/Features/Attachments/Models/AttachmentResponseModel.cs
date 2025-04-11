namespace Boardify.Application.Features.Attachments.Models
{
    public class AttachmentResponseModel
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int CardId { get; set; }
    }
}