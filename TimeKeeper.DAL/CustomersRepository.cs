using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class CustomersRepository : Repository<Customer>
    {
        public CustomersRepository(TimeKeeperContext context) : base(context) { }
    }
}
