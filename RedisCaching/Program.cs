using Microsoft.EntityFrameworkCore;
using RedisCaching.Context;
using RedisCaching.Interfaces;
using RedisCaching.Redis;
using RedisCaching.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redisConnection");
    options.InstanceName = "CarsCatelog_";
});

builder.Services.AddScoped<ICarsServices, CarsServices>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
});

builder.Services.AddScoped<RedisCacheService>();

builder.Services.AddDbContext<ContextClass>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
