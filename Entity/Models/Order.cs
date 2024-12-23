using grpcService.Entity;

public class Order : EntityBase
{
    public string? Name { get; set; } 
    public bool Enable { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}