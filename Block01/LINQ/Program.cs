using LINQ;
using LINQ.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString(nameof(ProductDbContext)));
    });


builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<OrdersRepository>();
builder.Services.AddScoped<DbSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
    
    var usersRepository = scope.ServiceProvider.GetRequiredService<UsersRepository>();

    var adultUsers = await usersRepository.FindAdults("SPB");
    foreach (var user in adultUsers)
    {
        Console.WriteLine(user.Name);
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();