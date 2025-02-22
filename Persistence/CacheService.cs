using Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<T?> GetAsync<T>(string cacheKey)
    {
        return Task.FromResult(_memoryCache.TryGetValue(cacheKey, out T cacheItem) ? cacheItem : default(T));
    }

    public Task AddAsync<T>(string cacheKey, T value, TimeSpan expireTimeSpan)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expireTimeSpan
        };
        _memoryCache.Set(cacheKey, value, cacheOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
        return Task.CompletedTask;
    }
}