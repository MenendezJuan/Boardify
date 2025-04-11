using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Users.Models;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Cards.Models
{
    public class CreateCardResponseModel
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public PriorityEnum? Priority { get; set; }
        public string? PriorityName { get; set; }
        public List<BoardLabelModel> Labels { get; set; } = new List<BoardLabelModel>();
        public List<CardAttachmentDetailModel> Attachments { get; set; } = new List<CardAttachmentDetailModel>();
        public List<UserResponseModel> Assignees { get; set; } = new List<UserResponseModel>();
        public UserResponseModel? Reporter { get; set; }
    }
}