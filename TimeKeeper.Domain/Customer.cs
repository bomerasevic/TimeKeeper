using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Customer : BaseClass
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public CustomerStatus Status { get; set; }
    }
}
