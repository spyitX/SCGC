using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.Extensions.Caching.Memory;
public class CacheProvider : ICacheProvider //this interface still can't see from latest Microsoft.Extensions.Caching.Memory (v 7.0)
{   
    private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
    private readonly IMemoryCache _cache;
    public CacheProvider(IMemoryCache memoryCache) {
        _cache = memoryCache;
    }
    public async Task < IEnumerable < Employee >> GetCachedResponse() {
        try {
            return await GetCachedResponse(CacheKeys.Employees, GetUsersSemaphore);
        } catch {
            throw;
        }
    }
    private async Task < IEnumerable < Employee >> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore) {
        bool isAvailable = _cache.TryGetValue(cacheKey, out List < Employee > employees);
        if (isAvailable) return employees;
        try {
            await semaphore.WaitAsync();
            isAvailable = _cache.TryGetValue(cacheKey, out employees);
            if (isAvailable) return employees;

            //employees = EmployeeService.GetEmployeesDetailsFromDB(); //Make this first for testing

            var cacheEntryOptions = new MemoryCacheEntryOptions {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
            };
            _cache.Set(cacheKey, employees, cacheEntryOptions);
        } catch {
            throw;
        } finally {
            semaphore.Release();
        }
        return employees;
    } 
}

