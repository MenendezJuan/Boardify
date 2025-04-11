using Boardify.Domain.Enums;

namespace Boardify.Application.Interfaces.Specific.Cards
{
    public interface IRecordCardActivity
    {
        Task LogActivityAsync(int cardId, int? userId, CardEventTypeEnum eventType, string details);
    }
}
