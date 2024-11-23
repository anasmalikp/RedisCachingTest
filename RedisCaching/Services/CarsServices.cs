using Microsoft.EntityFrameworkCore;
using RedisCaching.Context;
using RedisCaching.Interfaces;
using RedisCaching.Models;

namespace RedisCaching.Services
{
    public class CarsServices:ICarsServices
    {
        private readonly ContextClass context;
        public CarsServices(ContextClass context)
        {
            this.context = context;
        }

        public async Task<bool> AddCar(Cars car)
        {
            await context.cars.AddAsync(car);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cars>> GetAllCars()
        {
            return context.cars.ToList();
        }

        public async Task<Cars> GetById(int id)
        {
            return await context.cars.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
