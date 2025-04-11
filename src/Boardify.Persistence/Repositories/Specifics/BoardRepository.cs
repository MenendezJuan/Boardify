using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Domain.Entities;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        public BoardRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<Board?> GetBoardWithMembersAndWorkspacesAsync(int boardId)
        {
            return await _entities
                .Include(b => b.BoardMembers)
                .Include(b => b.Workspace)
                    .ThenInclude(w => w.UserWorkspaces)
                .FirstOrDefaultAsync(b => b.Id == boardId);
        }

        public async Task<Board?> GetByIdWithColumnsAndCardsAsync(int boardId)
        {
            return await _context.Boards
          .Include(b => b.Columns)
              .ThenInclude(c => c.Cards)
                  .ThenInclude(c => c.CardMembers)
                      .ThenInclude(cm => cm.User)
          .Include(b => b.Columns)
              .ThenInclude(c => c.Cards)
                  .ThenInclude(c => c.CardLabels) 
          .Include(b => b.BoardMembers)
          .FirstOrDefaultAsync(b => b.Id == boardId);
        }
    }
}