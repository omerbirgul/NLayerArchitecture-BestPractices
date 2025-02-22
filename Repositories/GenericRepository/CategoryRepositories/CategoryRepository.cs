using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories.GenericRepository.CategoryRepositories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductAsync(int id)
    {
        return  context.Categories
            .Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<Category> GetCategoryWithProduct()
    {
        return context.Categories.Include(x => x.Products).AsQueryable();
    }
}