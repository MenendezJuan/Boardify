using Boardify.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Base
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly DatabaseService _context;
        protected readonly DbSet<T> _entities;

        protected BaseRepository(DatabaseService context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>() ?? throw new ArgumentNullException();
        }
    }
}