﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
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

        [HttpGet("people")]
        public IActionResult GetPeople() => Ok(Unit.Employees.Get().Select(x => x.Master()).ToList());

        [HttpGet("calendar")]
        public IActionResult GetCalendar() => Ok(Unit.Calendar.Get().ToList());
    }
}