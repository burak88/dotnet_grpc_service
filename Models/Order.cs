public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; } 
    public bool Enable { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}