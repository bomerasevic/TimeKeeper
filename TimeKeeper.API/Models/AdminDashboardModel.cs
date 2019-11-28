using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class AdminDashboardModel
    {
        public AdminDashboardModel()
        {
            PTOHours = new List<TeamKeyDictionary>();
            OvertimeHours = new List<TeamKeyDictionary>();
            MissingEntries = new List<TeamKeyDictionary>();
        }
        public int NumberOfEmployees { get; set; }
        public int NumberOfProjects { get; set; }
        public int BaseTotalHours { get; set; }
        public int TotalHours { get; set; }
        public List<TeamKeyDictionary> PTOHours { get; set; }
        public List<TeamKeyDictionary> OvertimeHours { get; set; }
        public List<TeamKeyDictionary> MissingEntries { get; set; }
    }
}
