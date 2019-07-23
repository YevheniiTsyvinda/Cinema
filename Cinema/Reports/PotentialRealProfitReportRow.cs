using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Reports
{
    public class PotentialRealProfitReportRow
    {
        public  string Name { get; set; }

        public float GuaranteedProfit { get; set; }

        public float PotentialProfit { get; set; }
    }
}