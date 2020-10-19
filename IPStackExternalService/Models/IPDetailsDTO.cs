using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPStackExternalService.Models
{
    public class IPDetailsDTO : IPDetails
    {
        public string City { get; set; }
        [JsonProperty("country_name")]
        public string Country { get; set; }
        [JsonProperty("continent_name")]
        public string Contintent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
