using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class ProjectStatus   // provjeriti da li klasu treba proglasiti static
    {
        public int Id { get; set; }
        public const int IN_PROGRESS = 0;
        public const int ON_HOLD = 1;
        public const int FINISHED = 2;
        public const int CANCELED = 3;
    }
}
