using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Employee : BaseClass
    {
        public Employee()
        {
            Days = new List<Calendar>();
            Memberships = new List<Member>();
        }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual EmployeeStatus Status { get; set; }
        public virtual EmployeePosition Position { get; set; }
        public virtual IList<Calendar> Days { get; set; }
        public virtual IList<Member> Memberships { get; set; }

    }
}
