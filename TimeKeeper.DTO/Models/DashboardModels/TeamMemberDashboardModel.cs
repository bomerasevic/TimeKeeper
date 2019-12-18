using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class TeamMemberDashboardModel
    {
        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public decimal Overtime { get; set; }
        public decimal PaidTimeOff { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
