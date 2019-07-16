using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Reports
{
    public class BaseReportForm
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ReportType ReportType { get; set; }

        public virtual Dictionary<string,object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add(BaseReportFormConstants.DateFrom, DateFrom);
            parameters.Add(BaseReportFormConstants.DateTo, DateTo);
            parameters.Add(BaseReportFormConstants.ReportType, ReportType);

            return parameters;
        }
    }

    public static class BaseReportFormConstants
    {
        public static string DateFrom => "DateFrom";
        public static string DateTo => "DateTo";
        public static string ReportType => "ReportType";
    }
}