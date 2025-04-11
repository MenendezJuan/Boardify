using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class WorkspaceRepository : BaseRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<List<Workspace>> GetUserWorkspacesAsync(int userId)
        {
            return await _context.Workspaces
                .Where(w => w.UserWorkspaces.Any(uw => uw.UserId == userId && uw.IsOwner))
                .Include(w => w.UserWorkspaces)
                .Include(w => w.Boards)
                .ThenInclude(b => b.BoardMembers)
                .ToListAsync();
        }

        public async Task<List<Workspace>> GetGuestWorkspacesAsync(int userId)
        {
            return await _context.Workspaces
                .Where(w => w.UserWorkspaces.Any(uw => uw.UserId == userId && !uw.IsOwner))
                .Include(w => w.UserWorkspaces)
                .Include(w => w.Boards)
                .ThenInclude(b => b.BoardMembers)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetWorkspaceMemberCountsAsync(List<int> workspaceIds)
        {
            return await _context.UserWorkspaces
                .Where(uw => workspaceIds.Contains(uw.WorkspaceId))
                .GroupBy(uw => uw.WorkspaceId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Count()
                );
        }
    }
}