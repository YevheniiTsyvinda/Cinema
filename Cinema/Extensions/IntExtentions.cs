using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Extensions
{
    public static class IntExtentions
    {
        public static string ToDuration(this int value)
        {
            var hours = Math.Truncate(value / 60f);
            var minute = value % 60;

            return $"{hours}h {minute}m";
        }
    }
}