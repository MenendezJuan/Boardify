using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Cards.Commands
{
    public class RemoveMemberCommand : IRemoveCardMemberCommand
    {
        private readonly ICommandRepository<CardMember> _commandRepository;
        private readonly IQueryRepository<CardMember> _cardMemberQueryRepository;
        private readonly IResultFactory _resultFactory;
        private readonly IRecordCardActivity _recordCardActivity;

        public RemoveMemberCommand(
            ICommandRepository<CardMember> commandRepository,
            IQueryRepository<CardMember> cardMemberQueryRepository,
            IResultFactory resultFactory,
            IRecordCardActivity recordCardActivity)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _cardMemberQueryRepository = cardMemberQueryRepository ?? throw new ArgumentNullException(nameof(cardMemberQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
            _recordCardActivity = recordCardActivity ?? throw new ArgumentNullException(nameof(recordCardActivity));
        }

        public async Task<IResult<bool>> Delete(int cardId, int userId)
        {
            var cardMember = await _cardMemberQueryRepository.GetFirstOrDefaultAsync(cm => cm.CardId == cardId && cm.UserId == userId);

            if (cardMember == null)
            {
                return _resultFactory.Failure<bool>("CardMember not found.");
            }

            await _commandRepository.DeleteAsync(cardMember);

            await _recordCardActivity.LogActivityAsync(cardMember.CardId, cardMember.UserId, CardEventTypeEnum.MemberRemoved, "Miembro eliminado de la tarjeta.");

            return _resultFactory.Success(true);
        }
    }
}