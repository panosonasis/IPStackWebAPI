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
    public class JobProgressService : IJobProgressService
    {
        private readonly JobRepository _jobRepository;

        public JobProgressService(JobRepository JobRepository)
        {
            _jobRepository = JobRepository;
        }

        /// <summary>
        /// Create new Job with the total updates that contained 
        /// </summary>
        /// <param name="totalUpdates"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid> CreateNewJob(int totalUpdates,CancellationToken cancellationToken) =>
             (await _jobRepository.CreateJob(new Job() { HasFinished = false, CompletedUpdates = 0, TotalUpdates = totalUpdates,FailedUpdates = 0 }, cancellationToken)).JobId;

        /// <summary>
        /// Update successful item progress
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="updatesCompleted"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateJobProgress(Guid jobId, int updatesCompleted, CancellationToken cancellationToken) =>
            await _jobRepository.UpdateJob(jobId, updatesCompleted, cancellationToken);

        /// <summary>
        /// Update failed updated items
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="updatesFailed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateFailedJobProgress(Guid jobId, int updatesFailed, CancellationToken cancellationToken) =>
            await _jobRepository.UpdateJobFailedRequests(jobId, updatesFailed, cancellationToken);

        /// <summary>
        /// Get job progress by guid from db if is not terminated
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> GetJobProgress(Guid jobId, CancellationToken cancellationToken) =>
             await _jobRepository.GetJobProgressByGuid(jobId, cancellationToken);

        /// <summary>
        /// terminates job with a flag in db
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task EndJob(Guid jobId, CancellationToken cancellationToken) =>
            await _jobRepository.UpdateJob(jobId, true, cancellationToken);

    }
}
