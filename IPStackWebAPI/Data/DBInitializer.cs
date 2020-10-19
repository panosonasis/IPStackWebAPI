using IPStackExternalService.Models;
using IPStackWebAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPStackWebAPI.Data
{
    public class DBInitializer
    {
        public static void Seed(ApplicationContext context)
        {
            if (!context.IPDetails.Any())
            {
                context.AddRange
                    (
                        new IPDetailsExtDTO
                        { 
                            IP = "83.73.138.103",
                            City = "Århus",
                            Contintent = "Europe",
                            Country = "Denmark",
                            Latitude = 56.18429946899414,
                            Longitude = 10.112500190734863
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "81.73.138.103",
                            City = "Acilia-Castel Fusano-Ostia Antica",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 41.7755012512207,
                            Longitude = 12.306300163269043
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "81.63.138.103",
                            City = "Lucerne",
                            Contintent = "Europe",
                            Country = "Switzerland",
                            Latitude = 47.045658111572266,
                            Longitude = 8.308239936828613
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "81.63.128.103",
                            City = "Lausanne",
                            Contintent = "Europe",
                            Country = "Switzerland",
                            Latitude = 46.53950881958008,
                            Longitude = 6.646180152893066
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.63.128.103",
                            City = "Århus",
                            Contintent = "Europe",
                            Country = "Denmark",
                            Latitude = 56.158138275146484,
                            Longitude = 10.211999893188477
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.63.15.103",
                            City = "Ballerup",
                            Contintent = "Europe",
                            Country = "Denmark",
                            Latitude = 55.7244987487793,
                            Longitude = 12.3548002243042
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.63.20.103",
                            City = "Aulum",
                            Contintent = "Europe",
                            Country = "Denmark",
                            Latitude = 56.266700744628906,
                            Longitude = 8.800000190734863
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.63.10.103",
                            City = "Aulum",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 45.47200012207031,
                            Longitude = 45.47200012207031
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.23.20.103",
                            City = "Lombardy",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 45.47200012207031,
                            Longitude = 9.192000389099121
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.53.20.101",
                            City = "Lower Silesia",
                            Contintent = "Europe",
                            Country = "Poland",
                            Latitude = 51.114891052246094,
                            Longitude = 17.038040161132812
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.43.20.103",
                            City = "Irlam",
                            Contintent = "Europe",
                            Country = "United Kingdom",
                            Latitude = 53.43330001831055,
                            Longitude = -2.416599988937378
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "80.43.20.101",
                            City = "Irlam",
                            Contintent = "Europe",
                            Country = "United Kingdom",
                            Latitude = 53.43330001831055,
                            Longitude = -2.416599988937378
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.53.20.101",
                            City = "Limpiddu",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 40.69070053100586,
                            Longitude = 9.706199645996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.63.20.101",
                            City = "Olbia-Tempio",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 40.69070053100586,
                            Longitude = 9.706199645996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.53.10.101",
                            City = "Olbia-Tempio",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 39.69070053100586,
                            Longitude = 8.706199455996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.53.20.120",
                            City = "Olbia-Tempio",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 39.690700531009265,
                            Longitude = 8.706199645996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.59.20.120",
                            City = "Olbia-Tempio",
                            Contintent = "Europe",
                            Country = "Italy",
                            Latitude = 39.69070053100928,
                            Longitude = 8.708999645996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.59.49.120",
                            City = "Lower Silesia",
                            Contintent = "Europe",
                            Country = "Poland",
                            Latitude = 39.69037553100586,
                            Longitude = 8.706190945996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.59.79.120",
                            City = "Lower Silesia",
                            Contintent = "Europe",
                            Country = "Poland",
                            Latitude = 31.69070053100586,
                            Longitude = 8.706199095996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.59.99.120",
                            City = "Lower Silesia",
                            Contintent = "Europe",
                            Country = "Poland",
                            Latitude = 39.69070053100586,
                            Longitude = 8.706199645996094
                        },
                        new IPDetailsExtDTO
                        {
                            IP = "82.88.44.110",
                            City = "Lower Silesia",
                            Contintent = "Europe",
                            Country = "Poland",
                            Latitude = 40.69070053100586,
                            Longitude = 9.706199645996094
                        }

                    );

                context.SaveChanges();
            }
        }
    }
}
