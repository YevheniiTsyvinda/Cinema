using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Reports
{
    public class UnprofitableMoviesReportForm : BaseReportForm
    {
        public float Threshold { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            var parametrs = base.GetParameters();
            parametrs.Add(UnprofitableMoviesReportFormConstants.Threshold, Threshold);
            return parametrs;
        }
    }

    public static class UnprofitableMoviesReportFormConstants
    {
        public static string Threshold => "Threshold";
    }
}