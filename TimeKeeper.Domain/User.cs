using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class User : BaseClass
    {
        public string Name { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }  // rola na nivou aplikacije: admin, user, leader
    }
}
