using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class PersonalDashboardModel
    {
        public PersonalDashboardRawModel PersonalDashboardHours { get; set; }
        public decimal UtilizationMonthly { get; set; }
        public decimal UtilizationYearly { get; set; }
        public decimal BradfordFactor { get; set; }
    }
}
