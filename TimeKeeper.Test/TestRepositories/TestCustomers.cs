using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test.TestRepositories
{
    public class TestCustomers
    {
        static TimeKeeperContext _context;
        static IRepository<Customer> _customers;
    }
}
