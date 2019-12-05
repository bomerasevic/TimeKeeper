using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DTO.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            Projects = new List<MasterModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CustomerAddress Address { get; set; }
        public MasterModel Status { get; set; }
        public IList<MasterModel> Projects { get; set; }
    }
}
