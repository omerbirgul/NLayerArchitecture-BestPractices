using System.Linq.Expressions;

namespace Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    ValueTask<T?> GetByIdAsync(int id);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);


}