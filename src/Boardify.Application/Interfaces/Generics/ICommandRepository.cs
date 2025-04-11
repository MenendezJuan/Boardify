namespace Boardify.Application.Interfaces.Generics
{
    public interface ICommandRepository<in T> where T : class
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}