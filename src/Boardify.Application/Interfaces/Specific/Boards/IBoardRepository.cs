using Boardify.Domain.Entities;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IBoardRepository
    {
        Task<Board?> GetBoardWithMembersAndWorkspacesAsync(int boardId);

        Task<Board?> GetByIdWithColumnsAndCardsAsync(int boardId);
    }
}