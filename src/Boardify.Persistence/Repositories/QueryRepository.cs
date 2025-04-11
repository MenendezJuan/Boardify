using Boardify.Application.Interfaces.Generics;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Boardify.Persistence.Repositories
{
    public class QueryRepository<T> : BaseRepository<T>, IQueryRepository<T> where T : class
    {
        public QueryRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }
    }
}