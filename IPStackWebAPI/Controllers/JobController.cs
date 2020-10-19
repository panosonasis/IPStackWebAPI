using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IPStackWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IPStackWebAPI.Controllers
{
    public class JobController : BaseController
    {
        private readonly IJobProgressService _jobProgressService;

        public JobController(IJobProgressService jobProgressService)
        {
            _jobProgressService = jobProgressService;
        }

        /// <summary>
        /// EndPoint for getting the jobprogress by id
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetJobProgress(Guid guid, CancellationToken cancellationToken)
        {
            var progress = await _jobProgressService.GetJobProgress(guid, cancellationToken);
            if (!string.IsNullOrEmpty(progress))
                return Ok(progress);
            else
                return NotFound();
        }
    }
}
