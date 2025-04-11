using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Activity.Command
{
    public class RecordCardActivity : IRecordCardActivity
    {
        private readonly ICommandRepository<CardActivity> _commandRepository;

        public RecordCardActivity(ICommandRepository<CardActivity> commandRepository)
        {
            _commandRepository = commandRepository;
        }


        public async Task LogActivityAsync(int cardId, int? userId, CardEventTypeEnum eventType, string details)
        {
            var activity = new CardActivity
            {
                CardId = cardId,
                UserId = userId ?? 0,
                EventType = eventType,
                Activity = details,
                CreationTime = DateTime.UtcNow
            };

            await _commandRepository.InsertAsync(activity);
        }
    }
}
