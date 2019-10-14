using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Member : BaseClass
    {
        public string Description { get; set; }
        public int Hours { get; set; }
    }
}
