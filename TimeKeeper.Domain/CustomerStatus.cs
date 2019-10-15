using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class CustomerStatus
    {
        public CustomerStatus()
        {
            Customers = new List<Customer>();
        }
        public int Id { get; set; }
        public const int PROSPECT = 0;
        public const int CLIENT = 1;
        public virtual IList<Customer> Customers { get; set; }
    }
}
