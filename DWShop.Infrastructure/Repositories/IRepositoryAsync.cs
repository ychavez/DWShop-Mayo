using DWShop.Domain.Contracts;
using System.Linq.Expressions;

namespace DWShop.Infrastructure.Repositories
{
    public interface IRepositoryAsync<T, TId> where T : AuditableEntity<TId>
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        Task<List<T>> GetPagedAsync(int pageNumber, int pageSize,
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            params string[] IncludeStrings);

        Task SaveChangesAsync();

        Task UpdateAsync(T entity);
    }
}
