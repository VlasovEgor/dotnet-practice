using LINQ.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LINQ;

public class DbSeeder
{
    private readonly UsersRepository _usersRepository;
    private readonly OrdersRepository _ordersRepository;

    public DbSeeder(UsersRepository usersRepository, OrdersRepository ordersRepository)
    {
        _usersRepository = usersRepository;
        _ordersRepository = ordersRepository;
    }

    public async Task SeedAsync()
    {
        var existingUsers = await _usersRepository.Get();

        if (existingUsers.Count > 0)
            return;
        
        var firstUserId = Guid.NewGuid();
        var secondUserId = Guid.NewGuid();

        await _usersRepository.Add(firstUserId, "Alex", 19, "LA");
        await _usersRepository.Add(secondUserId, "Kate", 15, "SPB");
        
        await _ordersRepository.Add(
            firstUserId,
            Guid.NewGuid(),
            "Keyboard");

        await _ordersRepository.Add(
            firstUserId,
            Guid.NewGuid(),
            "Mouse");

        await _ordersRepository.Add(
            secondUserId,
            Guid.NewGuid(),
            "Monitor");
    }
}