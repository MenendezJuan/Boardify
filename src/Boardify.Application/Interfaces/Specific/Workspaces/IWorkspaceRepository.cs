using Boardify.Domain.Entities;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IWorkspaceRepository
    {
        Task<List<Workspace>> GetUserWorkspacesAsync(int userId);

        Task<List<Workspace>> GetGuestWorkspacesAsync(int userId);

        Task<Dictionary<int, int>> GetWorkspaceMemberCountsAsync(List<int> workspaceIds);
    }
}