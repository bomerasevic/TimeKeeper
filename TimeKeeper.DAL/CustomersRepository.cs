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

        public override void Update(Customer customer, int id)
        {
            Customer old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(customer);
                old.Address = customer.Address;
                old.Status = customer.Status;
            }
            else throw new ArgumentNullException();
        }
    }
}
