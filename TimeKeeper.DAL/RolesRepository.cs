using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class RolesRepository : Repository<Role>
    {
        public RolesRepository(TimeKeeperContext context) : base(context) { }

        public override void Delete(int id)
        {
            Role old = Get(id);

            if (old.MemberRoles.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}