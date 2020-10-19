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
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services
{
    public class IPStackService : IIPStackService
    {
        private readonly IIPServiceProvider _iPServiceProvider;
        private readonly IJobProgressService _jobService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public IBackgroundTaskQueue Queue { get; }

        public IPStackService(IIPServiceProvider iPServiceProvider, IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory,
            IJobProgressService jobProgressService)
        {
            _iPServiceProvider = iPServiceProvider;
            _jobService = jobProgressService;
            Queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IPDetailsDTO> GetIPDetails(string ip, CancellationToken cancellationToken)
        {
            return await _iPServiceProvider.GetIPDetails(ip, cancellationToken);
        }

        /// <summary>
        /// Splits the Update work into a new background service and returns the JobId for the background update
        /// </summary>
        /// <param name="iPsDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid> UpdateIPDetailsBatch(IEnumerable<IPDetailsExtDTO> iPsDetails, CancellationToken cancellationToken)
        {
            //Creates new instance of the current job in DB
            var jobCreatedId = await _jobService.CreateNewJob(iPsDetails.Count(), cancellationToken);

            //Runs the specified work on a background task
            Queue.QueueBackgroundWorkItem(async cancellationToken =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    await UpdateIPDetailsBatchInternal(iPsDetails, jobCreatedId, scopedServices, cancellationToken);
                }
            });

            return jobCreatedId;
        }

        private async Task UpdateIPDetailsBatchInternal(IEnumerable<IPDetailsExtDTO> iPsDetails, Guid jobId, IServiceProvider scopedServices,CancellationToken cancellationToken)
        {
            var iPStackRepository = scopedServices.GetRequiredService<IPStackRepository>();
            var iPDetailsBuffer = scopedServices.GetRequiredService<IIPDetailsBuffer>();
            var jobService = scopedServices.GetRequiredService<IJobProgressService>();

            //Initialize Buffer
            iPDetailsBuffer.Initliaze(iPsDetails);

            //Loop until there are no items in buffer
            while (iPDetailsBuffer.HasItems())
            {
                var batchForUpdate = iPDetailsBuffer.Take(10).ToList();
                //Update 10 each items of the current Job and the cache
                var success = await iPStackRepository.UpdateIPDetailsBatch(batchForUpdate, cancellationToken);
                if (success)
                    //Update Job Status in DB if no error 
                    await jobService.UpdateJobProgress(jobId, batchForUpdate.Count(), cancellationToken);
                else
                    await jobService.UpdateFailedJobProgress(jobId, batchForUpdate.Count(), cancellationToken);
            }

            //Job Finished
            await jobService.EndJob(jobId, cancellationToken);
        }
    }
}
