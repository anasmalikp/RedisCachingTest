using RedisCaching.Models;

namespace RedisCaching.Interfaces
{
    public interface ICarsServices
    {
        Task<bool> AddCar(Cars car);
        Task<List<Cars>> GetAllCars();
        Task<Cars> GetById(int id);
    }
}
