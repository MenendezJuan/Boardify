using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Domain.Relationships;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class BoardLabelRepository : BaseRepository<BoardLabel>, IBoardLabelRepository
    {
        public BoardLabelRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<bool> IsMemberOfBoardAsync(int boardLabelId, int boardId)
        {
            return await _entities.AnyAsync(bl => bl.Id == boardLabelId && bl.BoardId == boardId);
        }

        public async Task<IEnumerable<BoardLabel>> GetBoardLabels(int boardId)
        {
            return await _entities
                 .Where(ub => ub.BoardId == boardId)
                 .ToListAsync();
        }
    }
}