using IPStackExternalService.Models;
using IPStackWebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Repository
{
    public class CachedIPStackRepository : IIPStackRepository
    {
        private readonly IIPStackRepository _repository;
        private readonly IPMemoryCache _iPMemoryCache;

        public CachedIPStackRepository(IIPStackRepository repository, IPMemoryCache iPMemoryCache)
        {
            _repository = repository;
            _iPMemoryCache = iPMemoryCache;
        }

        public async Task<IPDetailsExtDTO> CreateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken) =>
            await _repository.CreateIPDetails(iPDetails,cancellationToken);


        public async Task<IPDetailsExtDTO> GetIPDetailsIfExist(string ip, CancellationToken cancellationToken) =>
            await _repository.GetIPDetailsIfExist(ip,cancellationToken);


        public async Task<bool> UpdateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken) =>
            await _repository.UpdateIPDetails(iPDetails, cancellationToken);

        public async Task<bool> UpdateIPDetailsBatch(List<IPDetailsExtDTO> iPDetails, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _repository.UpdateIPDetailsBatch(iPDetails, cancellationToken);
                foreach (var ipDetails in iPDetails)
                {
                    _iPMemoryCache.RemoveIfExists(ipDetails.IP);
                    _iPMemoryCache.TryCreateValue(ipDetails.IP, ipDetails);
                }
                return true && success;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
