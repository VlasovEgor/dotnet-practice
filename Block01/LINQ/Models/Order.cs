namespace LINQ.Models;

public class Order
{
    public Guid Id { get; set; }
    
    public string ProductName { get; set; }  = string.Empty;
    
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
}