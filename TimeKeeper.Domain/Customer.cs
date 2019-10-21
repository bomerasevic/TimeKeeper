using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Customer : BaseClass
    {
        public Customer()
        {
            Projects = new List<Project>();
        }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual CustomerAddress Address { get; set; }
        public virtual CustomerStatus Status { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
