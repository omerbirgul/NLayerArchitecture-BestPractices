namespace Services.Products.Dtos.Requests;

public record UpdateProductRequest(string Name, decimal Price, int Stock, int CategoryId);