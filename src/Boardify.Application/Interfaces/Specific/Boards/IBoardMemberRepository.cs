using Boardify.Domain.Relationships;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IBoardMemberRepository
    {
        Task<bool> IsMemberOfBoardAsync(int userId, int boardId);

        Task<IEnumerable<BoardMember>> GetBoardMembers(int boardId);
    }
}