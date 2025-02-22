namespace Domain.Entities;

public interface IAuditEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}