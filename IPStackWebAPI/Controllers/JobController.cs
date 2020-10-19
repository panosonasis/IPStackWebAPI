using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IPStackWebAPI.Controllers
{
    public class JobController : BaseController
    {
        public async Task<IActionResult> GetJobProgress(Guid guid)
        {
            return NotFound();
        }
    }
}
