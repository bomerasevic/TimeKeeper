using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL.Services;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        public ReportService reportService;
        public ReportController(TimeKeeperContext context) : base(context)
        {
            reportService = new ReportService(Unit);
        }
        [HttpGet("project-history-report/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProjectHistory(int projectId)
        {
            try
            {
                Log.Info($"Try to get project history for project with id:{projectId}");
                return Ok(reportService.GetProjectHistoryModel(projectId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("monthly-overview/{year}/{month}")]
        public IActionResult GetMonthlyOverview(int year, int month)
        {
            try
            {
                return Ok(reportService.GetMonthlyOverview(year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("annual-overview/{year}")]
        public IActionResult AnnualProjectOverview(int year)
        {
            try
            {
                return Ok(reportService.GetTotalAnnualOverview(year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}