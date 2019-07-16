using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Cinema
{
    public static class Constants
    {
        public static string ReportsDirectory => ConfigurationManager.AppSettings["ReportsDirectory"];
        public static string ExcelTemplateDirectory => ConfigurationManager.AppSettings["ExcelTemplateDirectory"];

    }
}