using System.Net;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.GenericRepository.ProductRepositories;
using Repositories.UnitOfWork;
using Services.Products.Dtos;
using Services.Products.Dtos.Requests;
using Services.Products.Dtos.Responses;

namespace Services.Products;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await _productRepository.GetTopPriceProductsAsync(count);
        // Manuel mapping yapıyoruz
        var productsAsDto = products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        
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

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
        return ServiceResult<ProductDto>.Success(productAsDto, HttpStatusCode.OK)!;
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAll().ToListAsync();
        var productsAsDto = products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedListAsync(int pageNumber, int pageSize)
    {
        // 1 -10 => 0-10 kayıt skip(0).Take(10)
        // 2 -10 => 11-20 kayıt skip(10).Take(10)
        // 3 -10 => 21-30 kayıt skip(20).Take(10)
        var skip = (pageNumber - 1) * pageSize;
        var products = await _productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();
        var productsAsDto = products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest productRequest)
    {
        var product = new Product()
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            Stock = productRequest.Stock
        };

        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>
            .SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }
    
    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product!.Name = updateProductRequest.Name;
        product.Price = updateProductRequest.Price;
        product.Stock = updateProductRequest.Stock;

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