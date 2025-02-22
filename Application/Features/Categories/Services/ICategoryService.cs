using Application.Features.Categories.Create;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Update;

namespace Application.Features.Categories.Services;

public interface ICategoryService
{
    Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
    Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
    Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
    Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
    Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request);
    Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
    Task<ServiceResult> DeleteAsync(int id);

}