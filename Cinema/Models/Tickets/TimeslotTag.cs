using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class TimeslotTag
    {
        public int TimeslotId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Cost { get; set; }
    }
}