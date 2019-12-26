using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DTO.Models.DomainModels
{
    public class DayModel
    {
        public DayModel()
        {
            Tasks = new List<AssignmentModel>();
        }
        public int Id { get; set; }
        public MasterModel Employee { get; set; }
        public MasterModel DayType { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalHours { get; set; }
        public List<AssignmentModel> Tasks { get; set; }
    }
}
