namespace Boardify.Application.Features.Attachments.Models
{
    public class UpdateAttachmentRequestModel
    {
        public int Id { get; set; }
        public string NewFileName { get; set; } = null!;
    }
}