using Application.Features.Products.Dtos;

namespace Application.Features.Categories.Dtos;

public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);