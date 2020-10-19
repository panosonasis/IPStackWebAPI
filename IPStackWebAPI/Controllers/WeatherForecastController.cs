using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IPStackExternalService.Models;
using IPStackExternalService.Services;
using IPStackWebAPI.Infrastructure;
using IPStackWebAPI.Repository;
using IPStackWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IPStackWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IIPInfoProvider _iIPInfoProvider;
        private readonly IIPStackService _repository;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IIPInfoProvider iIPInfoProvider, IIPStackService repository)
        {
            _logger = logger;
            _iIPInfoProvider = iIPInfoProvider;
            _repository = repository;
        }

        [HttpGet]
        public async  Task<Guid> Get()
        {
            var test1 = new List<int>() { 1, 2, 3, 4, 56, 67, 7, 7, 8, 9, 
                9, 23, 22, 26, 45, 4564, 5, 45, 45, 54,
                23, 23, 23, 223, 2 };

            var _ipDetailsQueue = new Queue<int>(test1);
            while (_ipDetailsQueue.Count() != 0)
            {
                var batchForUpdate = _ipDetailsQueue.DequeueChunk(10).ToList();

            }

            void test2(List<int> test3)
            {
                foreach (var item in test3)
                    Debug.WriteLine(item);
            };

            var actionBlock = new ActionBlock<List<int>>(n => 
            { 
                test2(n); 
            },new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 1,
                CancellationToken = CancellationToken.None,
                EnsureOrdered = true
            });

            for (int i = 0; i < test1.Count; i += 10)
            {
                actionBlock.Post(test1.Skip(i).Take(10).ToList());
                actionBlock.Complete();
                await actionBlock.Completion.ContinueWith(p => 
                {
                    if (p.Exception == null)
                    {
                        
                    }
                });
            }

            


            await _repository.GetIPDetails("85.73.138.103", CancellationToken.None);
            var ipInfo =  _iIPInfoProvider.GetDetails("85.73.138.103",CancellationToken.None);
            return Guid.NewGuid();
        }
    }
}
