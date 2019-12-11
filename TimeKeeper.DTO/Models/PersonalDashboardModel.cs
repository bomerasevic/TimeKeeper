using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.Models
{
    public class PersonalDashboardModel
    {
        public MasterModel Employee { get; set; }
        public decimal TotalHours { get; set; }
        public decimal WorkingHours { get; set; }
        public decimal BradfordFactor { get; set; }
    }
}
