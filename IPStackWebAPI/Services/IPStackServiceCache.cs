using IPStackExternalService.Models;
using IPStackWebAPI.Infrastructure;
using IPStackWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services
{
    public class IPStackServiceCache : IIPServiceProvider
    {
        private readonly IPMemoryCache _iPMemoryCache;
        private readonly IIPServiceProvider _iPStackServiceRepo;

        public IPStackServiceCache(IPMemoryCache iPMemoryCache, IIPServiceProvider iPStackServiceRepo)
        {
            _iPMemoryCache = iPMemoryCache;
            _iPStackServiceRepo = iPStackServiceRepo;
        }

        public async Task<IPDetailsDTO> GetIPDetails(string ip, CancellationToken cancellationToken)
        {
            //Check if value exists in cache
            IPDetailsDTO iPDetailsDTO;
            if (!_iPMemoryCache.TryGetIpDetails(ip,out iPDetailsDTO))
            {
                //Check if value exists in DB
                iPDetailsDTO = await _iPStackServiceRepo.GetIPDetails(ip, cancellationToken);
                _iPMemoryCache.TryCreateValue(ip, iPDetailsDTO);
                return iPDetailsDTO;
            }
            else
                //Value exist in cache
                return iPDetailsDTO;
        }
    }
}
