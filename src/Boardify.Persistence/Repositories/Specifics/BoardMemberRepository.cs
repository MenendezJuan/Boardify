using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class BoardMemberRepository : BaseRepository<BoardMember>, IBoardMemberRepository
    {
        public BoardMemberRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<bool> IsMemberOfBoardAsync(int userId, int boardId)
        {
            return await _entities.AnyAsync(bm => bm.UserId == userId && bm.BoardId == boardId);
        }

        public async Task<IEnumerable<BoardMember>> GetBoardMembers(int boardId)
        {
            return await _entities
                 .Where(ub => ub.BoardId == boardId)
                 .Include(ub => ub.User)
                 .ToListAsync();
        }

        public async Task<List<Board>> GetBoardsByWorkspaceIdAsync(int workspaceId)
        {
            return await _context.Boards
                .Where(b => b.WorkspaceId == workspaceId)
                .ToListAsync();
        }

        public async Task<List<Board>> GetBoardsForUserByWorkspaceIdAsync(int userId, int workspaceId)
        {
            return await _context.Boards
                .Where(b => b.WorkspaceId == workspaceId &&
                            (b.Visibility == VisibilityEnum.Workspace ||
                             b.Visibility == VisibilityEnum.Public ||
                             (b.Visibility == VisibilityEnum.Private && b.BoardMembers.Any(bm => bm.UserId == userId))))
                .ToListAsync();
        }
    }
}