using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProductAsync(int id);
    Task<List<Category>> GetCategoryWithProductAsync();
}