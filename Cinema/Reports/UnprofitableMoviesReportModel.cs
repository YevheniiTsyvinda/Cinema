using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Reports
{
    public class UnprofitableMoviesReportModel
    {
        public IEnumerable<UnprofitableMoviesReportRow> Rows { get; set; }
    }
}