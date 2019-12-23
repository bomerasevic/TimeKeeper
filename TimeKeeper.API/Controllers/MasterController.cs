using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DTO.Factory;
using TimeKeeper.DAL;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace TimeKeeper.API.Controllers
{
    //[Authorize(AuthenticationSchemes = "TokenAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : BaseController
    {
        public MasterController(TimeKeeperContext context) : base(context) { }

        [HttpGet("teams")]
        public IActionResult GetTeams() => Ok(Unit.Teams.Get().Select(x => x.Master()).ToList());

        [HttpGet("roles")]
        public IActionResult GetRoles() => Ok(Unit.Roles.Get().Select(x => x.Master()).ToList());

        [HttpGet("customers")]
        public IActionResult GetCustomers() => Ok(Unit.Customers.Get().Select(x => x.Master()).ToList());

        [HttpGet("projects")]
        public IActionResult GetProjects() => Ok(Unit.Projects.Get().Select(x => x.Master()).ToList());

        [HttpGet("employees")]
        public IActionResult GetPeople() => Ok(Unit.Employees.Get().Select(x => x.Master()).ToList());

        [HttpGet("calendar")]
        public IActionResult GetCalendar() => Ok(Unit.Calendar.Get().Select(x=>x.Master()).ToList());

        [HttpGet("members")]
        public IActionResult GetMembers() => Ok(Unit.Members.Get().ToList());

        [HttpGet("employeestatuses")]
        public IActionResult GetEmployeeStatuses() => Ok(Unit.EmployeeStatuses.Get().Select(s => s.Master()).ToList());

        [HttpGet("projectstatuses")]
        public IActionResult GetProjectStatuses() => Ok(Unit.ProjectStatuses.Get().Select(s => s.Master()).ToList());

        [HttpGet("projectprices")]
        public IActionResult GetProjectPrices() => Ok(Unit.ProjectPrices.Get().Select(s => s.Master()).ToList());

        [HttpGet("employeepositions")]
        public IActionResult GetEmployeePositions() => Ok(Unit.EmployeePositions.Get().Select(s => s.Master()).ToList());

        [HttpGet("daytypes")]
        public IActionResult GetDayTypes() => Ok(Unit.DayTypes.Get().Select(s => s.Master()).ToList());

        [HttpGet("memberstatuses")]
        public IActionResult GetMemberStatuses() => Ok(Unit.DayTypes.Get().Select(s => s.Master()).ToList());

        [HttpGet("customerstatuses")]
        public IActionResult GetCustomerStatuses() => Ok(Unit.CustomerStatuses.Get().Select(s => s.Master()).ToList());
    }
}