using LINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace LINQ.Repositories;

public class UsersRepository
{   
    private readonly ProductDbContext _dbContext;
    
    public UsersRepository(ProductDbContext context)
    {
        _dbContext = context;    
    }

    public async Task<List<User>> Get()
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<List<User>> GetWithOrdersWithoutInclude()
    {
        var users = await _dbContext.Users.ToListAsync();
        
        foreach (var user in users)
        {
            await _dbContext.Entry(user)
                .Collection(u => u.Orders)
                .LoadAsync();
        }
        
        return users;
    }
    
    public async Task<List<User>> GetWithOrdersWithInclude()
    {
        return await _dbContext
            .Users
            .Include(u => u.Orders)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await  _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Add(Guid id, string name, int age, string city)
    {
        var user = new User {Id = id, Name = name,  Age = age, City = city};
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> FindAdults(string city)
    {
        return await  _dbContext
            .Users
            .Where(u => u.Age >= 18 && u.City == city)
            .ToListAsync();
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.Users.
            Where(u => u.Id == id).
            ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
    }
}