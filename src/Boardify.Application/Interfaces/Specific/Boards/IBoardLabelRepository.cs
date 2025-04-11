using Boardify.Domain.Relationships;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IBoardLabelRepository
    {
        Task<bool> IsMemberOfBoardAsync(int boardLabelId, int boardId);

        Task<IEnumerable<BoardLabel>> GetBoardLabels(int boardId);
    }
}