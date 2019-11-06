using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.Utility
{
    public static class UsersUtility
    {
        public static User CreateUser(this Employee employee)
        {
            User user = new User
            {
                Name = employee.FullName,
                Username = (employee.FirstName + employee.LastName.Substring(0, 2)).ToLower(),
                Password = "$ch00l",
                Role = "user"
            };
            return user;
        }        
    }
}
