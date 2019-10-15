using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Role : BaseClass
    {
        public Role()
        {
            MemberRoles = new List<Member>();
        }
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public virtual IList<Member> MemberRoles { get; set; }
    }
}
