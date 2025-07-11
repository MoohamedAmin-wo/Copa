namespace Cupa.Infrastructure.Persistence.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    readonly DbSet<T> _entities;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    public async Task<int> GetCountAsync()
    {
        return await _entities.CountAsync();
    }

    public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.Where(predicate).CountAsync();
    }

    public async ValueTask<EntityEntry<T>> CreateAsync(T entity)
    {
        return await _entities.AddAsync(entity);
    }
    public ValueTask<EntityEntry<T>> UpdateAsync(T entity)
    {
        return ValueTask.FromResult(_entities.Update(entity));
    }
    public ValueTask<EntityEntry<T>> DeleteAsync(T entity)
    {
        return ValueTask.FromResult(_entities.Remove(entity));
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }
    public async Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
    {
        var query = _entities.AsQueryable();
        if (includes != null)
            query = includes(query);

        return await query.Where(predicate).FirstOrDefaultAsync();
    }
    public async Task<IReadOnlyCollection<T>?> FindMultipleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
    {
        var query = _entities.AsQueryable();
        if (includes != null)
            query = includes(query);

        return await query.Where(predicate).ToListAsync();
    }
    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool stopTracking = false)
    {
        var query = _entities.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (!stopTracking)
            query = query.AsNoTracking();

        if (includes != null)
            query = includes(query);

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool stopTracking = false, int take = 0, int skip = 0)
    {
        var query = _entities.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (!stopTracking)
            query = query.AsNoTracking();

        if (includes != null)
            query = includes(query);

        if (take > 0)
            query = query.Take(take);

        if (skip > 0)
            query = query.Skip(skip);

        return await query.ToListAsync();
    }

    public async Task<bool> HasAnyDataAsync()
    {
        return await _entities.AnyAsync() ? true : false;
    }
}
