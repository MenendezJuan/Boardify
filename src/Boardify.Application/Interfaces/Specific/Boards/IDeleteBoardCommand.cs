using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IDeleteBoardCommand
    {
        Task<IResult<bool>> Delete(int boardId, int userId);
    }
}