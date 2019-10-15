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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public CustomerAddress Address { get; set; }
        public CustomerStatus Status { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
