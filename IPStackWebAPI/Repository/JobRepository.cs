﻿using IPStackWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Repository
{
    public class JobRepository
    {
        private readonly ApplicationContext _appContext;

        public JobRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<string> GetJobProgressByGuid(Guid jobId, CancellationToken cancellationToken)
        {
            var job = await _appContext.Job.AsNoTracking().Where(x => x.JobId == jobId && x.HasFinished == false).SingleOrDefaultAsync(cancellationToken);
            if (job != null)
                return $"{job.CompletedUpdates}/{job.TotalUpdates}";
            else
                return string.Empty;
        }

        public async Task<Job> CreateJob(Job job, CancellationToken cancellationToken)
        {
            _appContext.Job.Add(job);
            await _appContext.SaveChangesAsync(cancellationToken);
            return job;
        }

        public async Task<Job> UpdateJob(Job job, CancellationToken cancellationToken)
        {
            _appContext.Entry(job).State = EntityState.Modified;
            await _appContext.SaveChangesAsync(cancellationToken);
            return job;
        }

        public async Task<bool> DeleteJobById(Guid jobId, CancellationToken cancellationToken)
        {
            var selectedJob = _appContext.Job.Find(jobId);
            _appContext.Job.Remove(selectedJob);
            await _appContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
