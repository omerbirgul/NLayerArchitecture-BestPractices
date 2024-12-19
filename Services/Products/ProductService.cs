using System.Net;
using Repositories.Entities;
using Repositories.GenericRepository.ProductRepositories;

namespace Services.Products;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ServiceResult<List<Product>>> GetTopPriceProductAsync(int count)
    {
        var products = await _productRepository.GetTopPriceProductsAsync(count);
        return new ServiceResult<List<Product>>()
        {
            Data = products,
            Status = HttpStatusCode.OK
        };
    }

    public async Task<ServiceResult<Product>> GetProductById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            ServiceResult<Product>.Fail("Product not found", HttpStatusCode.BadRequest);
        }

        return ServiceResult<Product>.Success(product!, HttpStatusCode.OK);
    }
}