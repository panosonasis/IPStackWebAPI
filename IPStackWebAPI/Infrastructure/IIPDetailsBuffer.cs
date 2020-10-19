using IPStackExternalService.Models;
using System.Collections.Generic;

namespace IPStackWebAPI.Infrastructure
{
    public interface IIPDetailsBuffer
    {
        void Initliaze(IEnumerable<IPDetailsExtDTO> iPDetailsExtDTOs);
        void Put(IPDetailsExtDTO iPDetailsExtDTOs);
        IEnumerable<IPDetailsExtDTO> Take(int chunkSize);
        bool HasItems();
    }
}