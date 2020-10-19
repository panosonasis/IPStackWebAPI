﻿using IPStackExternalService.Models;
using IPStackWebAPI.Infrastructure;
using IPStackWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Repository
{

    public class IPStackRepository
    {
        private readonly ApplicationContext _appContext;
        private readonly IPMemoryCache _iPMemoryCache;

        public IPStackRepository(ApplicationContext appContext, IPMemoryCache iPMemoryCache)
        {
            _appContext = appContext;
            _iPMemoryCache = iPMemoryCache;
        }

        public async Task<IPDetailsExtDTO> GetIPDetailsIfExist(string ip, CancellationToken cancellationToken)
        {
            var ipDetails = await _appContext.IPDetails.AsNoTracking().Where(x => x.IP == ip).SingleOrDefaultAsync(cancellationToken);
            return ipDetails;
        }

        public async Task<IPDetailsExtDTO> CreateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken)
        {
             _appContext.IPDetails.Add(iPDetails);
            await _appContext.SaveChangesAsync(cancellationToken);
            return iPDetails;
        }

        public async Task<bool> UpdateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken)
        {
            _appContext.Entry(iPDetails).State = EntityState.Modified;
            await _appContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateIPDetailsBatch(List<IPDetailsExtDTO> iPDetails, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in iPDetails)
                    _appContext.Entry(item).State = EntityState.Modified;

                await _appContext.SaveChangesAsync(cancellationToken);
                //Update cache
                UpdateIPDetailsCache(iPDetails);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void UpdateIPDetailsCache(List<IPDetailsExtDTO> iPsDetails)
        {
            foreach (var ipDetails in iPsDetails)
            {
                _iPMemoryCache.RemoveIfExists(ipDetails.IP);
                _iPMemoryCache.TryCreateValue(ipDetails.IP, ipDetails);            
            }
        }

    }
}
