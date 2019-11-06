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

        public void Build(Customer customer)
        {
            customer.Status = _context.CustomerStatuses.Find(customer.Status.Id);
        }
        public override void Insert(Customer customer)
        {
            Build(customer);
            base.Insert(customer);
        }
        public override void Update(Customer customer, int id)
        {
            Customer old = Get(id);

            Build(customer);
            _context.Entry(old).CurrentValues.SetValues(customer);
            old.Address = customer.Address;
            old.Status = customer.Status;
        }
        public override void Delete(int id)
        {
            Customer old = Get(id);

            if (old.Projects.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}
