﻿using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class MembersRepository : Repository<Member>
    {
        public MembersRepository(TimeKeeperContext context) : base(context) { }

    }
}