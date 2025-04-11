using Boardify.Domain.Entities;

namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface IUserQueryRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task<IEnumerable<User>> SearchUsersAsync(string searchText);

        Task<List<User>> GetBoardMembers(int boardId);

        Task<List<User>> GetWorkspaceMembers(int workspaceId);

        Task<List<User>> GetAssignedUsers(int cardId);
    }
}