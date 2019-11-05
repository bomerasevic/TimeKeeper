using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            MemberRoles = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }

        public List<MasterModel> MemberRoles { get; set; }
    }
}
