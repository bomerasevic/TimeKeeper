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
        public MonthlyReport monthlyReport;
        public AnnualReport annualReport;
        public ProjectHistoryReport projectHistoryReport;
        public ReportController(TimeKeeperContext context) : base(context)
        {
            monthlyReport = new MonthlyReport(Unit);
            annualReport = new AnnualReport(Unit);
            projectHistoryReport = new ProjectHistoryReport(Unit);
        }
        [HttpGet("project-history-report-stored/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProjectHistory(int projectId)
        {
            try
            {
                Log.Info($"Try to get project history for project with id:{projectId}");
                // return Ok(reportService.GetProjectHistoryModel(projectId));
                return Ok(projectHistoryReport.GetStoredProjectHistory(projectId));
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
                DateTime start = DateTime.Now;
                var ar = (new MonthlyReport(Unit)).GetMonthly(year, month);
                DateTime final = DateTime.Now;
                return Ok(new { dif = (final - start), ar });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("monthly-overview-stored/{year}/{month}")]
        public IActionResult GetStored(int year, int month)
        {
            try
            {
                DateTime start = DateTime.Now;
                var ar = (new MonthlyReport(Unit)).GetStored(year, month);
                DateTime final = DateTime.Now;
                return Ok(new { dif = (final - start), ar });
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
                return Ok(annualReport.GetAnnual(year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("annual-overview-stored/{year}")]
        public IActionResult GetStored(int year)
        {
            try
            {
                DateTime start = DateTime.Now;
                var ar = (new AnnualReport(Unit)).GetStored(year);
                DateTime final = DateTime.Now;
                return Ok(new { dif = (final - start), ar });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}