using IPStackExternalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Repository
{
    public interface IIPStackRepository
    {
        Task<IPDetailsExtDTO> GetIPDetailsIfExist(string ip, CancellationToken cancellationToken);
        Task<IPDetailsExtDTO> CreateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken);
        Task<bool> UpdateIPDetails(IPDetailsExtDTO iPDetails, CancellationToken cancellationToken);
        Task<bool> UpdateIPDetailsBatch(List<IPDetailsExtDTO> iPDetails, CancellationToken cancellationToken);
    }
}
