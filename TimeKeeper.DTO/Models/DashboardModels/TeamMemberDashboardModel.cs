using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class TeamMemberDashboardModel
    {
        public TeamMemberDashboardModel()
        {
            //WorkingHours = new Dictionary<string, decimal>();
            //MissingEntries = new Dictionary<int, decimal>();
        }
        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public decimal Overtime { get; set; }
        public decimal PaidTimeOff { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
