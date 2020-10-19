using IPStackExternalService.Models;
using IPStackExternalService.Services;
using IPStackWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services
{
    public class IPStackServiceExternalApi : IIPServiceProvider
    {
        private readonly IIPInfoProvider _iIPInfoProvider;

        public IPStackServiceExternalApi(IIPInfoProvider iIPInfoProvider)
        {
            _iIPInfoProvider = iIPInfoProvider;
        }

        public async Task<IPDetailsDTO> GetIPDetails(string ip, CancellationToken cancellationToken)
        {
            var ipDetails = await _iIPInfoProvider.GetDetails(ip, cancellationToken);
            return (IPDetailsDTO)ipDetails;
        }
    }
}
