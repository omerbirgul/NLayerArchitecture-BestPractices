using Services.Products.Dtos;
using Services.Products.Dtos.Requests;
using Services.Products.Dtos.Responses;

namespace Services.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);
    Task<ServiceResult<ProductDto?>> GetById(int id);
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedListAsync(int pageNumber, int pageSize);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest productRequest);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest);
    Task<ServiceResult> DeleteAsync(int id);
}