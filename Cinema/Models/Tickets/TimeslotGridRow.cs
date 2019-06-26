using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class TimeslotGridRow
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string MovieName { get; set; }
        public string HallName { get; set; }
        public string TariffName { get; set; }
    }
}