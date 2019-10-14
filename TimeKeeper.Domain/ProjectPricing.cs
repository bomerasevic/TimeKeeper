using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class ProjectPricing
    {
        public int Id { get; set; }
        public const string FixedBid = "fixed bid";
        public const string Hourly = "hourly";
        public const string PerCapita = "per capita";
    }
}
