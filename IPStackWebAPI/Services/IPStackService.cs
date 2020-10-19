using IPStackExternalService.Models;
using IPStackExternalService.Services;
using IPStackWebAPI.Infrastructure;
using IPStackWebAPI.Infrastructure.BackgroundServices;
using IPStackWebAPI.Models;
using IPStackWebAPI.Repository;
using IPStackWebAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services
{
    public class IPStackService : IIPStackService
    {
        private readonly IIPServiceProvider _iPServiceProvider;
        private readonly JobRepository _jobRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public IBackgroundTaskQueue Queue { get; }

        public IPStackService(IIPServiceProvider iPServiceProvider, IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory, 
            JobRepository jobRepository)
        {
            _iPServiceProvider = iPServiceProvider;
            _jobRepository = jobRepository;
            Queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IPDetailsDTO> GetIPDetails(string ip, CancellationToken cancellationToken)
        {
            return await _iPServiceProvider.GetIPDetails(ip, cancellationToken);
        }

        public async Task<Guid> UpdateIPDetailsBatch(IEnumerable<IPDetailsExtDTO> iPsDetails, CancellationToken cancellationToken)
        {
            //Creates new instance of the current job in DB
            var jobCreated = await _jobRepository.CreateJob(new Job() {CompletedUpdates = 0, HasFinished = false,TotalUpdates = iPsDetails.Count() }, cancellationToken);

            //Runs the specified work on a background task 
            Queue.QueueBackgroundWorkItem(async cancellationToken =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    await UpdateIPDetailsBatchInternal(iPsDetails, jobCreated, scopedServices, cancellationToken);
                }
            });
           
            return jobCreated.JobId;
        }

        private async Task UpdateIPDetailsBatchInternal(IEnumerable<IPDetailsExtDTO> iPsDetails, Job currentJob , IServiceProvider scopedServices, CancellationToken cancellationToken)
        {
            var iPStackRepository = scopedServices.GetRequiredService<IPStackRepository>();
            var iPDetailsBuffer = scopedServices.GetRequiredService<IIPDetailsBuffer>();
            var jobRepository = scopedServices.GetRequiredService<JobRepository>();

            //Initialize Buffer
            iPDetailsBuffer.Initliaze(iPsDetails);
            //await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            while (iPDetailsBuffer.HasItems())
            {
                var batchForUpdate = iPDetailsBuffer.Take(10);
                //Update 10 each items of the current Job and the cache
                var success = await iPStackRepository.UpdateIPDetailsBatch(batchForUpdate.ToList(), cancellationToken);
                if (success)
                {
                    //Update Job Status in DB
                    currentJob.CompletedUpdates += batchForUpdate.Count();
                    await jobRepository.UpdateJob(currentJob, cancellationToken);
                } 
            }

            currentJob.HasFinished = true;
            await jobRepository.UpdateJob(currentJob, cancellationToken);

        }

    }
}
