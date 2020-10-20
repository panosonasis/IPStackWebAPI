using IPStackExternalService.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPStackWebAPI.Infrastructure
{
    public class IPMemoryCache
    {
        private MemoryCache _cache;
        private static object _cacheLock = new object();

        public IPMemoryCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
            });
        }

        public bool TryCreateValue(string ip, IPDetailsDTO ipDetails)
        {
            lock (_cacheLock)
            {
                IPDetailsDTO ipDetailsCached;
                if (!_cache.TryGetValue(ip, out ipDetailsCached))
                {
                    //Key is not in cache , setting cache options
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
                    _cache.Set(ip, ipDetails, cacheEntryOptions);
                    return true;
                }
                else
                    return false;
            }
        }

        public bool TryGetIpDetails(string ip, out IPDetailsDTO ipDetailsCached)
        {
            lock (_cacheLock) 
            {
                return _cache.TryGetValue(ip, out ipDetailsCached);
            };
        }

        public void RemoveIfExists(string ip)
        {
            lock (_cacheLock)
            {
                IPDetailsDTO ipDetailsCached;
                if (_cache.TryGetValue(ip, out ipDetailsCached))
                    _cache.Remove(ip);
            }
        }
    }
}
