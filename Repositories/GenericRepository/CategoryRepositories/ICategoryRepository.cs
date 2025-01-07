using Repositories.Entities;

namespace Repositories.GenericRepository.CategoryRepositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProductAsync(int id);
    IQueryable<Category> GetCategoryByProductsAsync();
}