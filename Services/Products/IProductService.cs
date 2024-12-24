using Services.Products.Dtos;
using Services.Products.Dtos.Requests;
using Services.Products.Dtos.Responses;

namespace Services.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);
    Task<ServiceResult<ProductDto>> GetProductById(int id);
    Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest productRequest);
    Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest updateProductRequest);
    Task<ServiceResult> DeleteProductAsync(int id);
}