using Repositories.Entities;

namespace Repositories.GenericRepository.ProductRepositories;

public interface IProductRepository : IGenericRepository<Product>
{
    List<Product> GetTopPriceProductsAsync(int count);
}