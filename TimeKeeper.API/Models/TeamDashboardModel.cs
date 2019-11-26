using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TeamDashboardModel
    {
        public TeamDashboardModel()
        {
            EmployeePTO = new Dictionary<EmployeeModel, decimal>();
            EmployeeOvertime = new Dictionary<EmployeeModel, decimal>();
            EmployeeTimes = new List<EmployeeTimeModel>();
        }

        public int EmployeesCount { get; set; }
        public int ProjectsCount { get; set; }
        public int TotalHours { get; set; }
        public int WorkingHours { get; set; }

        public List<EmployeeTimeModel> EmployeeTimes { get; set; }
        public Dictionary<EmployeeModel, decimal> EmployeePTO { get; set; }
        public Dictionary<EmployeeModel, decimal> EmployeeOvertime { get; set; }
    }
}
