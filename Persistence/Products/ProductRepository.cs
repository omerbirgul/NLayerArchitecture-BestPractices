using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Products;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        return await _context.Products.OrderByDescending(x => x.Price).Take(5).ToListAsync();
    }

    public Task<List<Product>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return _context.Set<Product>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}

