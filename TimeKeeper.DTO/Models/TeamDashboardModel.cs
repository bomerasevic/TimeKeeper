using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models
{
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            EmployeePTO = new List<EmployeeKeyDictionary>();
            EmployeeOvertime = new List<EmployeeKeyDictionary>();
            EmployeeMissingEntries = new List<EmployeeKeyDictionary>();
        }

        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public int BaseTotalHours { get; set; }
        public int TotalHours { get; set; }
        public decimal WorkingHours { get; set; }
        public List<EmployeeKeyDictionary> EmployeePTO { get; set; }
        public List<EmployeeKeyDictionary> EmployeeOvertime { get; set; }
        public List<EmployeeKeyDictionary> EmployeeMissingEntries { get; set; }
    }
}
