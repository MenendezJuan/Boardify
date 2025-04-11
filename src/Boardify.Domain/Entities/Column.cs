using Boardify.Domain.BaseEntities;

namespace Boardify.Domain.Entities
{
    public class Column : BaseEntity
    {
        public int BoardId { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
        public Board Board { get; set; } = null!;
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}