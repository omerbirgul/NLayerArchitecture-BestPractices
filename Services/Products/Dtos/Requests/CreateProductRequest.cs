namespace Services.Products.Dtos.Requests;

public record CreateProductRequest(string Name, decimal Price, int Stock, int CategoryId);