using IPStackExternalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services.Interfaces
{
    public interface IIPStackService : IIPServiceProvider
    {
        Task<Guid> UpdateIPDetailsBatch(IEnumerable<IPDetailsExtDTO> iPsDetails, CancellationToken cancellationToken);
    }
}
