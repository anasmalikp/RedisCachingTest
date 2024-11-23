using Microsoft.EntityFrameworkCore;
using RedisCaching.Context;
using RedisCaching.Interfaces;
using RedisCaching.Redis;
using RedisCaching.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redisConnection");
    options.InstanceName = "CarsCatelog";
});

builder.Services.AddScoped<ICarsServices, CarsServices>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
});

builder.Services.AddScoped<RedisCacheService>();

builder.Services.AddDbContext<ContextClass>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
