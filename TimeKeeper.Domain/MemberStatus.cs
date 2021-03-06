﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class MemberStatus : BaseStatus
    {
        public MemberStatus()
        {
            Members = new List<Member>();
        }
        public virtual IList<Member> Members { get; set; }
    }
}
