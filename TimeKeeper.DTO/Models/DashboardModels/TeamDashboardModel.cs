using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class TeamRawPTOModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public decimal PaidTimeOff { get; set; }
    }
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            //TotalMissingEntries = new Dictionary<string, decimal>();
            EmployeeTimes = new List<TeamMemberDashboardModel>();
        }

        public int NumberOfEmployees { get; set; }
        public int NumberOfProjects { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        //public Dictionary<string, decimal> TotalMissingEntries { get; set; }
        public List<TeamMemberDashboardModel> EmployeeTimes { get; set; }
    }
}
