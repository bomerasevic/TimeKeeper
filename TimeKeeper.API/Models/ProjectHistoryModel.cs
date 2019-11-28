using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class ProjectHistoryModel
    {
        public ProjectHistoryModel()
        {
            Projects = new List<MasterModel>();
            Employees = new List<EmployeeProjectModel>();
        }
        public List<MasterModel> Projects { get; set; }
        public List<EmployeeProjectModel> Employees { get; set; }
        public Dictionary<int, decimal> TotalHoursPerProject { get; set; }
    }
}
