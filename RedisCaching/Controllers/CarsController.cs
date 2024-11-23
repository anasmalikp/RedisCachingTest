using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCaching.Interfaces;
using RedisCaching.Models;
using RedisCaching.Redis;

namespace RedisCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsServices services;
        private readonly RedisCacheService cacheService;
        public CarsController(ICarsServices services, RedisCacheService cacheService)
        {
            this.services = services;
            this.cacheService = cacheService;
        }


        [HttpPost]
        public async Task<IActionResult> PostCar(Cars car)
        {
            await services.AddCar(car);
            return Ok("Car Added");
        }

        [HttpGet]
        public async Task<IActionResult> AllCars()
        {
            var instanceId = GetInstanceId();
            var cacheKey = $"Cars_cache_{instanceId}";
            bool isFromCache = false;

            var cars = cacheService.GetcachedData<List<Cars>>(cacheKey);
            if(cars is null || cars.Count() < 1)
            {
                cars = await services.GetAllCars();
                cacheService.SetCachedData(cacheKey, cars, TimeSpan.FromMinutes(3));
                isFromCache = false;
            } else
            {
                isFromCache = true;
            }
            return Ok(new {Cars = cars, isFromCache = isFromCache});
        }

        private string GetInstanceId()
        {
            var instanceId = HttpContext.Session.GetString("InstanceId");
            if(string.IsNullOrEmpty(instanceId))
            {
                instanceId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("InstanceId", instanceId);
            }
            return instanceId;
        }
    }
}
