using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class MembersRepository : Repository<Member>
    {
        public MembersRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(Member member, int id)
        {
            Member old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(member);
                old.Team = member.Team;
                old.Employee = member.Employee;
                old.Role = member.Role;
                old.Status = member.Status;
            }
            else throw new ArgumentNullException();
        }
    }
}
