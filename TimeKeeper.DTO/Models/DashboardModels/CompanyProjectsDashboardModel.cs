﻿using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class CompanyProjectsDashboardModel
    {
        public MasterModel Project { get; set; }
        public decimal Revenue { get; set; }
    }
}