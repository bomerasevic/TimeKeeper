﻿using System;
using System.Collections.Generic;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public EmployeeStatus Status { get; set; }
        public EmployeePosition Position { get; set; }
        public virtual IList<Calendar> Days { get; set; }
        public virtual IList<Member> Memberships { get; set; }

    }
}
