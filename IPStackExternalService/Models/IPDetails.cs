using System;
using System.Collections.Generic;
using System.Text;

namespace IPStackExternalService.Models
{
    public interface IPDetails
    {
        string City { get; set; }
        string Country { get; set; }
        string Contintent { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
