using Boardify.Application.Features.Activity.Models;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Features.Users.Models;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Cards.Models
{
    public class BoardCardDetailResponse
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => _startDate = value == DateTime.MinValue ? null : value;
        }

        private DateTime? _dueDate;
        public DateTime? DueDate
        {
            get => _dueDate;
            set => _dueDate = value == DateTime.MinValue ? null : value;
        }

        public PriorityEnum Priority { get; set; }
        public string PriorityName => Priority.ToString();

        public List<BoardLabelModel> Labels { get; set; } = new List<BoardLabelModel>();
        public List<CardAttachmentDetailModel> Attachments { get; set; } = new List<CardAttachmentDetailModel>();
        public List<UserResponseModel> Assignee { get; set; } = new List<UserResponseModel>();
        public List<AddCommentResponseModel> Comments { get; set; } = new List<AddCommentResponseModel>();
        public List<CardActivityResponseModel> Activities { get; set; } = new List<CardActivityResponseModel>();
        public UserResponseModel Reporter { get; set; } = null!;
    }
}