using IPStackExternalService.Models;
using IPStackWebAPI.Models;
using IPStackWebAPI.Repository;
using IPStackWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services
{
    public class IPStackServiceRepo : IIPServiceProvider
    {
        private readonly IPStackRepository _iPStackRepository;
        private readonly IIPServiceProvider _iPStackServiceExternalApi;

        public IPStackServiceRepo(IPStackRepository iPStackRepository, IIPServiceProvider iPStackServiceExternalApi)
        {
            _iPStackRepository = iPStackRepository;
            _iPStackServiceExternalApi = iPStackServiceExternalApi;
        }

        public async Task<IPDetailsDTO> GetIPDetails(string ip, CancellationToken cancellationToken)
        {

            //Check if value exists in DB
            IPDetailsDTO ipDetailsEntity = await _iPStackRepository.GetIPDetailsIfExist(ip, cancellationToken);

            if (ipDetailsEntity == null)
            {
                var ipDetails = await _iPStackServiceExternalApi.GetIPDetails(ip, cancellationToken);

                IPDetailsExtDTO temp = new IPDetailsExtDTO() 
                {
                    IP = ip, City = ipDetails.City, 
                    Contintent = ipDetails.Contintent,   Country = ipDetails.Country, 
                    Latitude = ipDetails.Latitude, Longitude = ipDetails.Longitude 
                };

                //create db record
                ipDetailsEntity = await _iPStackRepository.CreateIPDetails(temp, cancellationToken);

                return ipDetailsEntity;
            }
            else
                return ipDetailsEntity;

        }
    }
}
