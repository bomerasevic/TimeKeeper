using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DTO.Models
{
    public class ProjectHistoryModel
    {
        public ProjectHistoryModel()
        {
            Employees = new List<EmployeeProjectHistoryModel>();
            Years = new List<int>();
        }
        public List<int> Years { get; set; }
        public List<EmployeeProjectHistoryModel> Employees { get; set; }
    }
}
