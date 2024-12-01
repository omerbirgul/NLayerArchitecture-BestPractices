using System.Linq.Expressions;

namespace Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    ValueTask<T?> GetByIdAsync(int id);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);

}