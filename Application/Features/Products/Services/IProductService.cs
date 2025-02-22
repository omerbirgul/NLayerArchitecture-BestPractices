using Application.Features.Products.Create;
using Application.Features.Products.Dtos;
using Application.Features.Products.Update;

namespace Application.Features.Products.Services;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);
    Task<ServiceResult<ProductDto?>> GetById(int id);
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedListAsync(int pageNumber, int pageSize);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest productRequest);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest);
    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
    Task<ServiceResult> DeleteAsync(int id);
}