using Boardify.Domain.Enums;

namespace Boardify.Domain.BaseEntities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime LastModifiedTime { get; set; }
        public StatusEnum Status { get; set; }
    }
}