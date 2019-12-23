using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.ReportModels
{
    public class EmployeeMonthlyProjectModel
    {
        public EmployeeMonthlyProjectModel()
        {
            HoursByProject = new Dictionary<string, decimal>();
        }

        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public decimal PaidTimeOff { get; set; }
        public Dictionary<string, decimal> HoursByProject { get; set; }
    }
}
