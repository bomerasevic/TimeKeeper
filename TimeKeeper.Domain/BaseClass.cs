using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class BaseClass
    {
        public BaseClass()
        {
            Created = DateTime.Now;
            Creator = 0;
            Deleted = false;
        }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Creator { get; set; }
        public bool Deleted { get; set; }
    }
}
