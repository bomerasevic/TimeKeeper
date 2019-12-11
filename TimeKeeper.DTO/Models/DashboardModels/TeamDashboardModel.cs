using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            //EmployeePTO = new List<EmployeeKeyDictionary>();
            //EmployeeOvertime = new List<EmployeeKeyDictionary>();
            //EmployeeMissingEntries = new List<EmployeeKeyDictionary>();
            EmployeeTimes = new List<TeamMemberDashboardModel>();
        }

        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public int BaseTotalHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal TotalMissingEntries { get; set; }
        public List<TeamMemberDashboardModel> EmployeeTimes { get; set; }
        //public List<EmployeeKeyDictionary> EmployeePTO { get; set; }
        //public List<EmployeeKeyDictionary> EmployeeOvertime { get; set; }
        //public List<EmployeeKeyDictionary> EmployeeMissingEntries { get; set; }
    }
}
