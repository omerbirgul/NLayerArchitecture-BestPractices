using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.GenericRepository.CategoryRepositories;
using Repositories.UnitOfWork;
using Services.Categories.Dtos;
using Services.Categories.Dtos.Create;
using Services.Categories.Dtos.Update;

namespace Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAll().ToListAsync();
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoryDtos);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);
        }

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryDto);
    }

    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryWithProductAsync(categoryId);
        if (category is null)
        {
            return ServiceResult<CategoryWithProductsDto>.Fail("Category not found", HttpStatusCode.NotFound);
        }

        var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);
        return ServiceResult<CategoryWithProductsDto>.Success(categoryDto);
    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
    {
        var category = await _categoryRepository.GetCategoryWithProduct().ToListAsync();
        var categoryDto = _mapper.Map<List<CategoryWithProductsDto>>(category);
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryDto);
    }

    public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
    {
        var anyCategory = await _categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
        if(anyCategory)
        {
            return ServiceResult<CreateCategoryResponse>.Fail("Category already exist");
        }

        var category = _mapper.Map<Category>(request);
        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateCategoryResponse>
            .SuccessAsCreated(new CreateCategoryResponse(category.Id), $"api/categories/{category.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
        }

        var isCategoryExist = await _categoryRepository
            .Where(x => x.Name == request.Name && category.Id != x.Id).AnyAsync();
        if (isCategoryExist)
        {
            return ServiceResult.Fail("Category name already exist");
        }

        category = _mapper.Map(request, category);
        _categoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult.Fail("category not found");
        }
        _categoryRepository.Delete(category);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}