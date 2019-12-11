using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models
{
    public class ProjectHistoryRawData
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Hours { get; set; }
        public int Year { get; set; }
    }
    public class EmployeeProjectHistoryModel
    {
        public EmployeeProjectHistoryModel(List<int> years)
        {
            TotalYearlyProjectHours = new Dictionary<int, decimal>();
            foreach (int i in years) TotalYearlyProjectHours.Add(i, 0);
        }
        public MasterModel Employee { get; set; }
        public Dictionary<int, decimal> TotalYearlyProjectHours { get; set; }
        public decimal TotalHoursPerProject { get; set; }
    }
}
