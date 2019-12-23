using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.BLL.Services;
using TimeKeeper.DAL;
using TimeKeeper.Utility;

namespace TimeKeeper.API.Controllers
{
    [Authorize(AuthenticationSchemes ="TokenAuthentication")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class BaseController : ControllerBase
    {
        protected UnitOfWork Unit;
        public LoggerService Log;
        protected readonly AccessHandler Access;
        public BaseController(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
            Log = new LoggerService();
            Access = new AccessHandler(Unit);
        }

        public IActionResult HandleException(Exception e)
        {            
            if (e is ArgumentException)
            {
                Log.Error(e.Message);
                return NotFound(e.Message);
            }

            Log.Fatal(e.Message);
            return BadRequest(e.Message);
        }
    }
}