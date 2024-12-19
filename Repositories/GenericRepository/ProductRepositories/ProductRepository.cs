using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories.GenericRepository.ProductRepositories;

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
}

