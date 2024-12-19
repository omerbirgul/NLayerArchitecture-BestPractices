using Repositories.GenericRepository.ProductRepositories;

namespace Services.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    
}