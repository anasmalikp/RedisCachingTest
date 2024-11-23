using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisCaching.Redis
{
    public class RedisCacheService
    {
        private readonly IDistributedCache cache;
        public RedisCacheService(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public T GetcachedData<T>(string key)
        {
            var jsonData = cache.GetString(key);
            if(jsonData is null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData)!;
        }

        public void SetCachedData<T>(string key, T data, TimeSpan cacheDuraction)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuraction,
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };

            var jsonData = JsonSerializer.Serialize(data);

            cache.SetString(key, jsonData, options);
        }
    }
}
