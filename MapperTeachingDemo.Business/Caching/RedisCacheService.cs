using MapperTeachingDemo.WebAPI.Cashing;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MapperTeachingDemo.WebAPI.Caching;

public class RedisCacheService :ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> Get<T>(string key)
    {
       var json = await _cache.GetStringAsync(key);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public async Task Set<T>(string key, T value, TimeSpan expiry)
    {
       var  json = JsonSerializer.Serialize(value); 
        await _cache.SetStringAsync(key, json,new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow =expiry
        });
    }
}
