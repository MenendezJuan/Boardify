namespace Boardify.Application.Features.Attachments.Models
{
    public class CardAttachmentModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}