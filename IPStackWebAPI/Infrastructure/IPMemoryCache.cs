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

        public IPMemoryCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                //SizeLimit = 1024
            });
        }

        public bool TryCreateValue(string ip, IPDetailsDTO ipDetails)
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

        //TODO remove
        public IPDetailsDTO GetValueSetIfExists(string ip)
        {
            IPDetailsDTO ipDetailsCached;

            if (!_cache.TryGetValue(ip, out ipDetailsCached))    
                return null; //Key is not in cache
            else
                return ipDetailsCached; //Key is in cache 
        }

        public bool TryGetIpDetails(string ip, out IPDetailsDTO ipDetailsCached) => _cache.TryGetValue(ip, out ipDetailsCached);

        public void RemoveIfExists(string ip)
        {
            IPDetailsDTO ipDetailsCached;
            if (_cache.TryGetValue(ip, out ipDetailsCached))
                _cache.Remove(ip);
        }
    }
}
