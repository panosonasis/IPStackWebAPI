using IPStackExternalService.Models;
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

    public class IPStackRepository : IIPStackRepository
    {
        private readonly ApplicationContext _appContext;

        public IPStackRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IPDetailsExtDTO> GetIPDetailsIfExist(string ip, CancellationToken cancellationToken)
        {
            var ipDetails = await _appContext.IPDetails.AsNoTracking().Where(x => x.IP == ip).SingleOrDefaultAsync(cancellationToken);
            return ipDetails;
        }

        public async Task<IPDetailsExtDTO> CreateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken)
        {
             var obj = _appContext.IPDetails.Find(iPDetails.IP);
            if (obj == null)
            {
                _appContext.IPDetails.Add(iPDetails);
                await _appContext.SaveChangesAsync(cancellationToken);
                return iPDetails;
            }
            else
                return obj;
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
