using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

using AutoMapper;
using Puma.Prey.Rabbit.Models;

namespace Coyote.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger logger;
        protected IMapper mapper;
    }
}