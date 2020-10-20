using IPStackExternalService.Exceptions;
using IPStackExternalService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackExternalService.Services
{
    public class IPInfoProvider : IIPInfoProvider
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private string _accessKey;
        private string _baseUrl;


        public IPInfoProvider(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _config = config;
            _clientFactory = clientFactory;
            _accessKey = _config["IpStackAPI:AccessKey"];
            _baseUrl = _config["IpStackAPI:BaseUrl"]; 
        }

        /// <summary>
        /// Makes a request to an external API returning data for a specific IP if successful
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  async  Task<IPDetails> GetDetails(string ip, CancellationToken cancellationToken)
        {
            try
            {
                IPDetailsDTO ipDetails;
                IPDetailsError ipDetailsError;
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{ip}?access_key={_accessKey}");
                var ipStackClient = _clientFactory.CreateClient();
                var ipStackResponse = await ipStackClient.SendAsync(request, cancellationToken);

                if (ipStackResponse.IsSuccessStatusCode)
                {
                    var responseStream = await ipStackResponse.Content.ReadAsStringAsync();
                    ipDetailsError = JsonConvert.DeserializeObject<IPDetailsError>(responseStream);
                    if (!ipDetailsError.success && ipDetailsError.error != null)
                        throw new IPServiceNotAvailableException(ipDetailsError.error.info);

                    ipDetails = JsonConvert.DeserializeObject<IPDetailsDTO>(responseStream);
                }
                else
                    throw new IPServiceNotAvailableException(await ipStackResponse.Content.ReadAsStringAsync());

                return ipDetails;
            }
            catch (Exception ex)
            {
                throw new IPServiceNotAvailableException(ex.Message,ex.InnerException);
            }
           
        }
    }
}
