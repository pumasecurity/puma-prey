using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;

using Puma.Prey.Rabbit.EF;
using Puma.Prey.Rabbit.Models;

namespace Coyote.Controllers.Home
{
    public class HomeAPIController: BaseAPIController
    {
        private readonly ILogger logger;

        public HomeAPIController(RabbitDBContext db, IMapper mapper, ILoggerFactory logger) : base(db, mapper)
        {
            this.logger = logger.CreateLogger<HomeAPIController>();
        }

         [HttpGet]
        //http://localhost:7862/HomeAPI/?HuntId=1
        public async Task<Hunt> Index(int HuntId)
        {
            var result = await db.Hunts.Include(p => p.Animals).FirstOrDefaultAsync(p => p.Id == HuntId);
            return result;
        } 
    }
}