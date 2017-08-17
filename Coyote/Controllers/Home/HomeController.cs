using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoMapper;

using Puma.Prey.Rabbit.EF;
using Puma.Prey.Rabbit.Models;
using Coyote.Models.HomeViewModels;

namespace Coyote.Controllers.Home
{
    public class HomeController : BaseController
    {
        private HomeAPIController api;

        public HomeController(RabbitDBContext db, IMapper mapper, ILoggerFactory logger)
        {
            this.mapper = mapper;
            this.logger = logger.CreateLogger<HomeController>();
            this.api = new HomeAPIController(db, mapper, logger);
        }
        public async Task<IActionResult> Index()
        {
            var hunt = await api.Index(1);
            var result = mapper.Map<Hunt, HuntViewModel>(hunt);
            return View(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
