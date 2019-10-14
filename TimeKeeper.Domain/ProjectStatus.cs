using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public const string InProgress = "in progress";
        public const string OnHold = "on hold";
        public const string Finished = "finished";
        public const string Canceled = "canceled";
    }
}
