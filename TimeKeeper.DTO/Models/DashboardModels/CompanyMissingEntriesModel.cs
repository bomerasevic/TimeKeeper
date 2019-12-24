using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class CompanyMissingEntriesModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal MissingEntriesHours { get; set; }
    }
}
