using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Cards
{
    public interface IRemoveCardMemberCommand
    {
        Task<IResult<bool>> Delete(int cardId, int memberId);
    }
}