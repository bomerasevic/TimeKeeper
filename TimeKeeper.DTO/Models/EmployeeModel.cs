using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            Days = new List<MasterModel>();
            Memberships = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public MasterModel Status { get; set; }
        public MasterModel Position { get; set; }

        public List<MasterModel> Days { get; set; }
        public List<MasterModel> Memberships { get; set; }
    }
}
