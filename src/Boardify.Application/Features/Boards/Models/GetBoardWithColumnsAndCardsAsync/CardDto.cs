using Boardify.Application.Features.Users.Models;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync
{
    public class CardDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int ColumnId { get; set; }
        public PriorityEnum Priority { get; set; }
        public string? PriorityName { get; set; } 
        public List<UserCardMemberModel> Assignees { get; set; } = new List<UserCardMemberModel>();
        public int CommentCount { get; set; }
        public int AttachmentCount { get; set; }
        public string? AttachmentPreview { get; set; }
        public List<int> Labels { get; set; } = new List<int>();
    }
}