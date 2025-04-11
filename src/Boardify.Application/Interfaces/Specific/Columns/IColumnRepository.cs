using Boardify.Domain.Entities;
using System.Linq.Expressions;

namespace Boardify.Application.Interfaces.Specific.Columns
{
    public interface IColumnRepository
    {
        Task<IEnumerable<Column>> GetColumnsAsync(Expression<Func<Column, bool>>? predicate = null);

        Task<Column?> GetByIdWithCardsAsync(int columnId);

        Task<IEnumerable<Column>> GetColumnsByBoardIdAsync(int boardId);

        Task UpdateColumnsAsync(IEnumerable<Column> columns);
    }
}