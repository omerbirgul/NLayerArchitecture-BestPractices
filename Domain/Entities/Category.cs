using Domain.Entities;

namespace Domain.Entities;

public class Category : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}