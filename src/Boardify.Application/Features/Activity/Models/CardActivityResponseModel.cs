using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Activity.Models
{
    public class CardActivityResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public CardEventTypeEnum EventType { get; set; }
        public string Details { get; set; } = null!;
        public DateTime CreationTime { get; set; }
    }
}
