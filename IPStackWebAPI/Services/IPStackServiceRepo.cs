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
        private readonly IIPServiceProvider _iPServiceProvider;

        public IPStackServiceRepo(IPStackRepository iPStackRepository, IIPServiceProvider iPServiceProvider)
        {
            _iPStackRepository = iPStackRepository;
            _iPServiceProvider = iPServiceProvider;
        }

        public async Task<IPDetailsDTO> GetIPDetails(string ip, CancellationToken cancellationToken)
        {

            //Check if value exists in DB
            IPDetailsDTO ipDetailsEntity = await _iPStackRepository.GetIPDetailsIfExist(ip, cancellationToken);

            if (ipDetailsEntity == null)
            {
                var ipDetails = await _iPServiceProvider.GetIPDetails(ip, cancellationToken);

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
