using Microsoft.EntityFrameworkCore;
using ProductRESTfulAPI.DbContexts;

namespace ProductRESTfulAPI.Services;

public abstract class GenericRepository<T>
    : IRepository<T> where T : class
{
    protected AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual T Add(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity));
        return _context.Add(entity).Entity;
    }

    public virtual async Task<T?> GetAsync(Guid id)
    {
        var result = await _context.FindAsync<T>(id);
        return result;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _context.Set<T>().ToListAsync();
        return result;
    }

    public virtual T Update(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity));
        return _context.Update(entity).Entity;
    }

    public virtual T Delete(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity));
        return _context.Remove(entity).Entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
