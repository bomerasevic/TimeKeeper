using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DTO.Models.ReportModels
{
    public class ProjectHistoryModel
    {
        public ProjectHistoryModel()
        {
            Employees = new List<EmployeeProjectModel>();
            TotalYearlyProjectHours = new Dictionary<int, decimal>();
        }
        public List<EmployeeProjectModel> Employees { get; set; }
        public Dictionary<int, decimal> TotalYearlyProjectHours { get; set; }
        public decimal TotalHoursPerProject { get; set; }
    }
}
