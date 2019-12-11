using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DashboardModels;

namespace TimeKeeper.DTO.Models
{
    public class AdminDashboardModel
    {
        public AdminDashboardModel()
        {
            //PTOHours = new List<TeamKeyDictionary>();
            //OvertimeHours = new List<TeamKeyDictionary>();
            //MissingEntries = new List<TeamKeyDictionary>();
            Teams = new List<AdminTeamDashboardModel>();
        }
        public int NumberOfEmployees { get; set; }
        public int NumberOfProjects { get; set; }
        public decimal BaseTotalHours { get; set; }
        public decimal TotalHours { get; set; }
        public List<AdminTeamDashboardModel> Teams { get; set; }
        //public List<TeamKeyDictionary> PTOHours { get; set; }
        //public List<TeamKeyDictionary> OvertimeHours { get; set; }
        //public List<TeamKeyDictionary> MissingEntries { get; set; }
    }
}
