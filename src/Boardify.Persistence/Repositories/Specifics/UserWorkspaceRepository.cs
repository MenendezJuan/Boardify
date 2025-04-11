using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class UserWorkspaceRepository : BaseRepository<UserWorkspace>, IUserWorkspaceRepository
    {
        public UserWorkspaceRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<IEnumerable<UserWorkspace>> GetWorkspaceMembers(int workspaceId)
        {
            return await _entities
                .Where(uw => uw.WorkspaceId == workspaceId)
                .Include(uw => uw.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserWorkspace>> GetUserWorkspacesAsync(int userId)
        {
            return await _entities
                .Where(uw => uw.UserId == userId)
                .Include(uw => uw.Workspace)
                .ToListAsync();
        }

        public async Task<bool> IsOwnerOfWorkspace(int userId, int workspaceId)
        {
            var userWorkspaces = await GetUserWorkspacesAsync(userId);
            return userWorkspaces.Any(uw => uw.WorkspaceId == workspaceId && uw.IsOwner);
        }

        public async Task<IEnumerable<Workspace>> GetOwnedWorkspacesAsync(int userId)
        {
            return await _entities
                .Where(uw => uw.UserId == userId && uw.IsOwner)
                .Select(uw => uw.Workspace)
                .ToListAsync();
        }
    }
}