using Boardify.Application.Interfaces.Specific.Columns;
using Boardify.Domain.Entities;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class ColumnRepository : BaseRepository<Column>, IColumnRepository
    {
        public ColumnRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<IEnumerable<Column>> GetColumnsAsync(Expression<Func<Column, bool>>? predicate = null)
        {
            IQueryable<Column> query = _entities;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<Column?> GetByIdWithCardsAsync(int columnId)
        {
            return await _entities
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(c => c.Id == columnId);
        }

        public async Task<IEnumerable<Column>> GetColumnsByBoardIdAsync(int boardId)
        {
            return await _entities
                .Where(c => c.BoardId == boardId)
                .OrderBy(c => c.Position)
                .ToListAsync();
        }

        public async Task UpdateColumnsAsync(IEnumerable<Column> columns)
        {
            _entities.UpdateRange(columns);
            await _context.SaveChangesAsync();
        }
    }
}