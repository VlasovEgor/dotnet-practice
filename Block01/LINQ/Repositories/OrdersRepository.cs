using LINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace LINQ.Repositories;

public class OrdersRepository
{
    private readonly ProductDbContext _dbContext;

    public OrdersRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Guid userId, Guid orderId, string productName)
    {       
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId)
                   ?? throw new Exception("User not found");

        Order order = new()
        {
            UserId = userId,
            User = user,
            ProductName = productName,
            Id = orderId,
        };
        
        _dbContext.Orders.Add(order);
        
        var savedEntries = await _dbContext.SaveChangesAsync();
    }

}