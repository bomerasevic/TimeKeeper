using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class EmployeeMissingEntries
    {
        public EmployeeMissingEntries()
        {
            Employee = new MasterModel();
        }
        public MasterModel Employee { get; set; }
        public decimal MissingEntries { get; set; }
    }
}
