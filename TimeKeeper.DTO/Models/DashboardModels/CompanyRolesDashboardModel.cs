using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class CompanyRolesDashboardModel
    {
        public MasterModel Role { get; set; }
        public decimal TotalHours { get; set; }
        public decimal WorkingHours { get; set; }
    }
}
