using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.ReportModels
{
    public class MonthlyRawData
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int ProjId { get; set; }
        public string ProjName { get; set; }
        public decimal Hours { get; set; }
    }
    public class ProjectMonthlyModel
    {
        public ProjectMonthlyModel()
        {
            Projects = new List<MasterModel>();
            Employees = new List<EmployeeProjectModel>();
        }
        public List<MasterModel> Projects { get; set; }
        public List<EmployeeProjectModel> Employees { get; set; }
    }
}
