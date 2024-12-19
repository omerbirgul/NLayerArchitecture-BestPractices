using Repositories.Entities;

namespace Repositories.GenericRepository.ProductRepositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetTopPriceProductsAsync(int count);
}