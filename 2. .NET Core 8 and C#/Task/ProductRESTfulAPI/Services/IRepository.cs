namespace ProductRESTfulAPI.Services;

public interface IRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
    Task<T?> GetAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveChangesAsync();
}