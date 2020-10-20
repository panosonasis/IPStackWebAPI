using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPStackExternalService;
using IPStackExternalService.Services;
using IPStackWebAPI.Data;
using IPStackWebAPI.Infrastructure;
using IPStackWebAPI.Infrastructure.BackgroundServices;
using IPStackWebAPI.Middleware;
using IPStackWebAPI.Repository;
using IPStackWebAPI.Services;
using IPStackWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IPStackWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            //register Infrastructure classes
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddSingleton<IPMemoryCache>();
            services.AddScoped<IIPDetailsBuffer, IPDetailsBuffer>();
            //Register DB Connection string
            services.AddDbContext<ApplicationContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });

            services.AddTransient(FactoryRepositoryDecorator);
            services.AddTransient<JobRepository>();

            //Register types of external service dll
            ModuleRegistration.RegisterTypes(services);

            //Used a decorator pattern
            services.AddScoped(FactoryDecorator);
            services.AddScoped<IJobProgressService, JobProgressService>();
            services.AddScoped<IIPStackService, IPStackService>();

            services.AddControllers();
        }

        private IIPStackRepository FactoryRepositoryDecorator(IServiceProvider arg)
        {
            return new CachedIPStackRepository(new IPStackRepository(arg.GetService<ApplicationContext>()), arg.GetService<IPMemoryCache>());
        }

        private IIPServiceProvider FactoryDecorator(IServiceProvider arg)
        {
            return new IPStackServiceCache(arg.GetService<IPMemoryCache>(),new IPStackServiceRepo(arg.GetService<IIPStackRepository>(), 
                new IPStackServiceExternalApi(arg.GetService<IIPInfoProvider>())));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Error Handling Middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Seed DB if table is empty
            DBInitializer.Seed(serviceProvider.GetRequiredService<ApplicationContext>());
        }
    }
}
