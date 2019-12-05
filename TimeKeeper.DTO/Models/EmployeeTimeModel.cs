﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models
{
    public class EmployeeTimeModel
    {
        public EmployeeTimeModel()
        {
            HourTypes = new Dictionary<string, decimal>();
        }
        public MasterModel Employee { get; set; }
        public decimal PTO { get; set; }  // paidtimeoff
        public decimal OverTime { get; set; }
        public Dictionary<string,decimal> HourTypes { get; set; }        
    }
}
