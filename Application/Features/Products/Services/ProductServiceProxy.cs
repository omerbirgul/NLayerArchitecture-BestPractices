using Application.Contracts.Caching;
using Application.Features.Products.Create;
using Application.Features.Products.Dtos;
using Application.Features.Products.Update;

namespace Application.Features.Products.Services;

public class ProductServiceProxy : IProductService
{
    private readonly ProductService _productService;
    private readonly ICacheService _cacheService;

    public ProductServiceProxy(ProductService productService, ICacheService cacheService)
    {
        _productService = productService;
        _cacheService = cacheService;
    }

    private const string productListCacheKey = "ProductListCacheKey";
 
    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var cachedData = await _cacheService.GetAsync<List<ProductDto>>(productListCacheKey);
        if (cachedData is not null)
            return ServiceResult<List<ProductDto>>.Success(cachedData);

        var result = await _productService.GetAllAsync();
        if (result.IsSuccess)
            await _cacheService.AddAsync(productListCacheKey, result.Data, TimeSpan.FromMinutes(1));
        return result;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count) => 
        await _productService.GetTopPriceProductAsync(count);


    public async Task<ServiceResult<ProductDto?>> GetById(int id) => await _productService.GetById(id);

    public async Task<ServiceResult<List<ProductDto>>> GetPagedListAsync(int pageNumber, int pageSize) =>
        await _productService.GetPagedListAsync(pageNumber, pageSize);

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest productRequest) =>
        await _productService.CreateAsync(productRequest);

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest) =>
        await _productService.UpdateAsync(id, updateProductRequest);

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request) =>
        await _productService.UpdateStockAsync(request);

    public async Task<ServiceResult> DeleteAsync(int id) => await _productService.DeleteAsync(id);
}