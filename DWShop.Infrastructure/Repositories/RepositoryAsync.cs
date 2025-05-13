using DWShop.Domain.Contracts;
using DWShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DWShop.Infrastructure.Repositories
{
    public class RepositoryAsync<T, TId>(AuditableContext context)
        : IRepositoryAsync<T, TId> where T : AuditableEntity<TId>
    {
        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(TId id)
        => await context.Set<T>().FindAsync(id);

        public async Task<List<T>> GetPagedAsync(int pageNumber, int pageSize, 
            Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, 
            params string[] IncludeStrings)
        {
            IQueryable<T> query = context.Set<T>();

            // agregamos los join
            query = IncludeStrings.Aggregate(query,
                (current, itemInclude) => current.Include(itemInclude));

            // si hubo where lo agregamos
            if (predicate is not null)
                query = query.Where(predicate);

            //Paginacion
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            //revisamos ordenamiento y retornamos
            return await (orderBy is not null ? orderBy(query) : query).ToListAsync();

        }


        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();

        public Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
