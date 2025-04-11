using Boardify.Application.Interfaces.Generics;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;

namespace Boardify.Persistence.Repositories
{
    public class CommandRepository<T> : BaseRepository<T>, ICommandRepository<T> where T : class
    {
        public CommandRepository(DatabaseService context) : base(context)
        {
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}