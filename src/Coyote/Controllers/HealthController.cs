using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private static string _version = "1.0";

        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Status")]
        [AllowAnonymous]
        public ActionResult<Dictionary<string, object>> Status()
        {
            //Get status details
            var version = Environment.GetEnvironmentVariable("VERSION");
            version = !string.IsNullOrEmpty(version) ? version : _version;

            var collection = new Dictionary<string, object>();
            collection.Add("Version", version);

            return new JsonResult(collection);
        }

        [HttpPost("Status")]
        public ActionResult<Dictionary<string, object>> AuthStatus()
        {
            //Get status details
            var version = Environment.GetEnvironmentVariable("VERSION");
            version = !string.IsNullOrEmpty(version) ? version : _version;

            var collection = new Dictionary<string, object>();
            collection.Add("Version", version);

            return new JsonResult(collection);
        }
    }
}
