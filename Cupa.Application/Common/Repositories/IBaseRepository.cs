using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cupa.Application.Common.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<int> GetCountAsync();
    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);
    Task<bool> HasAnyDataAsync();
    ValueTask<EntityEntry<T>> CreateAsync(T entity);
    ValueTask<EntityEntry<T>> UpdateAsync(T entity);
    ValueTask<EntityEntry<T>> DeleteAsync(T entity);
    Task<T?> FindByIdAsync(int id);
    Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    Task<IReadOnlyCollection<T>?> FindMultipleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool stopTracking = false);
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool stopTracking = false, int take = 0, int skip = 0);
}
