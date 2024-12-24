namespace Services.Products.Dtos.Requests;

public record UpdateProductRequest(int Id, string Name, decimal Price, int Stock);