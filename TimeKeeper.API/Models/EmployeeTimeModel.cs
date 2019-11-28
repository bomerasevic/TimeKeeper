using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class EmployeeTimeModel
    {
        public EmployeeTimeModel()
        {
            HourTypes = new Dictionary<string, decimal>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PTO { get; set; }  // paidtimeoff
        public decimal OverTime { get; set; }
        public Dictionary<string,decimal> HourTypes { get; set; }
    }
}
