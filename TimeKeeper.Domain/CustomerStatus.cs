using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class CustomerStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
