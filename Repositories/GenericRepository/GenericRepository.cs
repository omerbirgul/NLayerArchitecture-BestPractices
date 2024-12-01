using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<T> GetAll() => _context.Set<T>().AsQueryable().AsNoTracking();

    public ValueTask<T?> GetByIdAsync(int id) => _context.Set<T>().FindAsync(id);

    public async ValueTask AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate).AsNoTracking();
}