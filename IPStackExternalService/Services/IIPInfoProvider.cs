using IPStackExternalService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackExternalService.Services
{
    public interface IIPInfoProvider
    {
        Task<IPDetails> GetDetails(string ip, CancellationToken cancellationToken);
    }
}
