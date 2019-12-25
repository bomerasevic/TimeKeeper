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

        public override void Delete(int id)
        {
            Customer old = Get(id);

            if (old.Projects.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}
