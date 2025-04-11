using Boardify.Domain.BaseEntities;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Domain.Entities
{
    public class Card : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int ColumnId { get; set; }
        public Column Column { get; set; } = null!;
        public List<CardMember> CardMembers { get; set; } = new List<CardMember>();
        public List<CardAttachment> CardAttachments { get; set; } = new List<CardAttachment>();
        public List<ChecklistItem> ChecklistItem { get; set; } = new List<ChecklistItem>();
        public List<CardLabel> CardLabels { get; set; } = new List<CardLabel>();
        public List<CardComment> CardComments { get; set; } = new List<CardComment>();
        public List<CardActivity> CardActivities { get; set; } = new List<CardActivity>();
        public int? ReporterId { get; set; }
        public User? Reporter { get; set; }
        public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;
    }
}