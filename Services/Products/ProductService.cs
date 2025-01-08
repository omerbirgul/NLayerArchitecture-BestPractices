using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.GenericRepository.CategoryRepositories;
using Repositories.GenericRepository.ProductRepositories;
using Repositories.UnitOfWork;
using Services.ExceptionHandlers;
using Services.Products.Dtos;
using Services.Products.Dtos.Requests;
using Services.Products.Dtos.Responses;

namespace Services.Products;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await _productRepository.GetTopPriceProductsAsync(count);

        #region Manuel Mapping

        // var productsAsDto = products
        //     .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        #endregion

        var productsAsDto = _mapper.Map<List<ProductDto>>(products);
        
        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto,
            Status = HttpStatusCode.OK
        };
    }

    public async Task<ServiceResult<ProductDto?>> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
        }

        #region Manuel Mapping

        // var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        #endregion

        var productAsDto = _mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productAsDto, HttpStatusCode.OK)!;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAll().ToListAsync();

        #region Manuel Mapping

        // var productsAsDto = products
        //     .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        #endregion

        var productsAsDto = _mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedListAsync(int pageNumber, int pageSize)
    {
        // 1 -10 => 0-10 kayıt skip(0).Take(10)
        // 2 -10 => 11-20 kayıt skip(10).Take(10)
        // 3 -10 => 21-30 kayıt skip(20).Take(10)
        var skip = (pageNumber - 1) * pageSize;
        var products = await _productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();
        if (!products.Any())
        {
            return ServiceResult<List<ProductDto>>.Fail("Products Not Found");
        }

        #region Manuel Mapping

        // var productsAsDto = products
        //     .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        #endregion

        var productsAsDto = _mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var existProduct = await _productRepository.Where(x => x.Name == request.Name).AnyAsync();
        if (existProduct)
        {
            return ServiceResult<CreateProductResponse>.Fail("Product Already Exist");
        }

        var isCategoryExist = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (isCategoryExist is null)
        {
            return ServiceResult<CreateProductResponse>.Fail("Category not found");
        }

        var product = _mapper.Map<Product>(request);
        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>
            .SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }
    
    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        bool isProductExist = await _productRepository
            .Where(x => x.Name == request.Name && x.Id != product.Id).AnyAsync();
        if (isProductExist)
        {
            return ServiceResult.Fail("Product name already exist");
        }
        
        product = _mapper.Map(request, product);
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }
        product.Stock = request.Quantity;
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("product not found", HttpStatusCode.NotFound);
        }
        _productRepository.Delete(product!);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}