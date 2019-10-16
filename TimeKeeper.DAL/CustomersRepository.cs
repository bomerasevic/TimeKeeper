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

        public override IList<Customer> Get(Func<Customer, bool> where) => Get().Where(where).ToList();
        public override Customer Get(int id) => Get().FirstOrDefault(x => x.Id == id);
    }
}
