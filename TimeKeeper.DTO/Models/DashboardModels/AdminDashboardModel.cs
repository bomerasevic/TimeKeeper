using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DashboardModels;

namespace TimeKeeper.DTO.Models
{    
    public class AdminDashboardModel
    {
        public AdminDashboardModel(List<string> roles)
        {
            PaidTimeOff = new List<AdminRawPTOModel>();
            Teams = new List<AdminTeamDashboardModel>();
            Projects = new List<AdminProjectDashboardModel>();
            Roles = new List<AdminRolesDashboardModel>();
            Roles.AddRange(roles.Select(x => new AdminRolesDashboardModel
            {
                RoleName = x
            }));
        }
        public int NumberOfEmployees { get; set; }
        public int NumberOfProjects { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }

        public List<AdminRawPTOModel> PaidTimeOff { get; set; }
        public List<AdminTeamDashboardModel> Teams { get; set; }
        public List<AdminProjectDashboardModel> Projects { get; set; }
        public List<AdminRolesDashboardModel> Roles { get; set; }

        // missingentries dodati
    }
}
