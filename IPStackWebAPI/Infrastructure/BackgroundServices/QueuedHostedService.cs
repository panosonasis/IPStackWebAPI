using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace IPStackWebAPI.Infrastructure.BackgroundServices
{
    public class QueuedHostedService : BackgroundService
    {

        private Task _backgroundTask;
        private readonly ILogger _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, ILoggerFactory loggerFactory)
        {
            TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<QueuedHostedService>();
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        /// <summary>
        /// Background Tasks are dequeued and executed as a BackgroundService
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (false == stoppingToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(stoppingToken);
                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, $"Error occurred executing {nameof(workItem)}.");
                }
            }
        }
    }
}
