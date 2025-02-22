using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductAsync(int id)
    {
        return  context.Categories
            .Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Category>> GetCategoryWithProductAsync()
    {
        return context.Categories.Include(x => x.Products).ToListAsync();
    }
}