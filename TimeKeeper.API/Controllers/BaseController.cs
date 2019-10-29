using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DAL;
using TimeKeeper.LOG;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UnitOfWork Unit;
        public LoggerService Log = new LoggerService();
        public BaseController(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
    }
}