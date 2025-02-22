using System.Linq.Expressions;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<List<T>> GetAllAsync() => _context.Set<T>().ToListAsync();

    public ValueTask<T?> GetByIdAsync(int id) => _context.Set<T>().FindAsync(id);

    public async ValueTask AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate).AsNoTracking();
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _context.Set<T>().AnyAsync(predicate);
}