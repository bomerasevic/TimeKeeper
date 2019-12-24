﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class CompanyDashboardModel
    {
        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public List<CompanyMissingEntriesModel> MissingEntries { get; set; }
        public List<CompanyTeamModel> Teams { get; set; }
        public List<CompanyProjectsDashboardModel> Projects { get; set; }
        public List<CompanyRolesDashboardModel> Roles { get; set; }
    }
}
