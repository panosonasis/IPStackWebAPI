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
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationContext context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationContext>();

            if (context.IPDetails.Any())
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
                            IP = "82.73.138.103",
                            City = "",
                            Contintent = "Europe",
                            Country = "",
                            Latitude = 2.2,
                            Longitude = 2.2
                        }

                    );

                context.SaveChanges();
            }
        }
    }
}
