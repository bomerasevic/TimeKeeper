using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models.ReportModels
{
    public class TeamTimeTrackingModel
    {
        public TeamTimeTrackingModel()
        {
            Employees = new List<EmployeeTimeModel>();
        }
        public MasterModel Team { get; set; }
        public IList<EmployeeTimeModel> Employees { get; set; }
    }
}
