using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPStackExternalService.Models;
using IPStackExternalService.Services;
using IPStackWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IPStackWebAPI.Controllers
{
    public class IPStackController : BaseController
    {
        private readonly IIPStackService _iPStackService;
        public IPStackController(IIPStackService iPStackService)
        {
            _iPStackService = iPStackService;
        }

        /// <summary>
        /// Endpoint for get IP Details from db,cache or External API
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        async public Task<IActionResult>  GetIPDetails(string ip, CancellationToken cancellationToken)
        {
            return Ok(await _iPStackService.GetIPDetails(ip, cancellationToken));
        }

        /// <summary>
        /// Endpoint for Batch update
        /// </summary>
        /// <param name="iPDetailsExts"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<IActionResult> UpdateIPDetails(IEnumerable<IPDetailsExtDTO> iPDetailsExts, CancellationToken cancellationToken)
        {
            return Ok(await _iPStackService.UpdateIPDetailsBatch(iPDetailsExts, cancellationToken));
        }
    }
}
