﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Coyote.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        public AboutController()
        {
        }

        [HttpGet("{aboutFile}")]
        public async Task<ActionResult> GetAbout(string aboutFile)
        {
              return this.Redirect(aboutFile);
        }
    }
}