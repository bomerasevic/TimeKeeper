using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class CustomerAddress
    {
        public CustomerAddress()
        {
            Customers = new List<Customer>();
        }
        public string Road { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual IList<Customer> Customers { get; set; }
    }
}
