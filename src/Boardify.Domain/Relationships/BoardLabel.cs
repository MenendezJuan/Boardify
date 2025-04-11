using Boardify.Domain.BaseEntities;
using Boardify.Domain.Entities;

namespace Boardify.Domain.Relationships
{
    public class BoardLabel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
        public List<CardLabel> CardLabels { get; set; } = new List<CardLabel>();
    }
}