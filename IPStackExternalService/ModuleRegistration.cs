using IPStackExternalService.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IPStackExternalService
{
    public class ModuleRegistration
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<IIPInfoProvider, IPInfoProvider>();
        }
    }
}
