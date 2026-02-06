using System.Linq.Expressions;

namespace TaskManager.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<IEnumerable<T>> GetPagedAsync(
            Expression<Func<T, bool>>? filter,
            int pageNumber,
            int pageSize);

        Task SaveChangesAsync();
    }
}
