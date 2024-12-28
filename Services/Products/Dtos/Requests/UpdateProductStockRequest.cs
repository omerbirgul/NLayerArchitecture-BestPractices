namespace Services.Products.Dtos.Requests;

public record UpdateProductStockRequest(int ProductId, int Quantity);