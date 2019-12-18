using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DashboardModels;

namespace TimeKeeper.DTO.Models
{
    public class RawMasterModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal Value { get; set; }
    }
    public class RawCountModel
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public decimal WorkingHours { get; set; }
    }
    public class AdminRawPTOModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public decimal PaidTimeOff { get; set; }
    }
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
