using DevFreela.Core.Services;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;

        private readonly DistributedCacheEntryOptions _options;
        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions {

                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration=TimeSpan.FromMinutes(5)
            };
        }

        public string Get(string key)
        {
           return  _cache.GetString(key);
        }

        public async Task<string> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public void Refresh(string key)
        {
            _cache.Refresh(key);
        }

        public async  Task RefreshAsync(string key)
        {
            await _cache.RefreshAsync(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public async  Task RemoveAsync(string key)
        {
            await _cache.RefreshAsync(key);
        }

        public void Set(string key, string value)
        {
            _cache.SetString(key, value);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }
    }
}
