using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IUserWorkspaceRepository
    {
        Task<IEnumerable<UserWorkspace>> GetWorkspaceMembers(int workspaceId);

        Task<IEnumerable<UserWorkspace>> GetUserWorkspacesAsync(int userId);

        Task<bool> IsOwnerOfWorkspace(int userId, int workspaceId);

        Task<IEnumerable<Workspace>> GetOwnedWorkspacesAsync(int userId);
    }
}