using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

using AutoMapper;

using Puma.Prey.Rabbit.EF;

namespace Coyote.Controllers
{
    public class BaseAPIController
    {
        protected RabbitDBContext db;
        protected IMapper mapper;

        public BaseAPIController(RabbitDBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
    }
}