using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{    
    public class TeamMissingEntries
    {
        public TeamMissingEntries()
        {
            Team = new MasterModel();
        }
        public MasterModel Team { get; set; }
        public decimal Hours { get; set; }
    }
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            EmployeeTimes = new List<TeamMemberDashboardModel>();
        }
        public int Year { get; set; }
        public int Month { get; set; }
        public MasterModel Team { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfProjects { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public List<TeamMemberDashboardModel> EmployeeTimes { get; set; }
    }
}
