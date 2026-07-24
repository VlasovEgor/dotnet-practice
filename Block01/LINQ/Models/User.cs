namespace LINQ.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public string City { get; set; } = string.Empty;

    public List<Order> Orders { get; set; } = new();
}