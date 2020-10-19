using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPStackWebAPI.Services.Interfaces
{
    public interface IJobProgressService
    {
        Task<Guid> CreateNewJob(int totalUpdates, CancellationToken cancellationToken);
        Task<string> GetJobProgress(Guid jobId, CancellationToken cancellationToken);
        Task UpdateJobProgress(Guid jobId, int updatesCompleted, CancellationToken cancellationToken);
        Task UpdateFailedJobProgress(Guid jobId, int updatesFailed, CancellationToken cancellationToken);
        Task EndJob(Guid jobId, CancellationToken cancellationToken);
    }
}
