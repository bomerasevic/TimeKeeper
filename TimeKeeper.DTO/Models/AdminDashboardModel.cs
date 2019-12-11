using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models
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
        public decimal BaseTotalHours { get; set; }
        public decimal TotalHours { get; set; }
        public List<TeamKeyDictionary> PTOHours { get; set; }
        public List<TeamKeyDictionary> OvertimeHours { get; set; }
        public List<TeamKeyDictionary> MissingEntries { get; set; }
    }
}
