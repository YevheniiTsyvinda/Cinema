using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class TicketsJsonModel
    {
        public Movie[] Movies { get; set; }
        public Hall[] Halls { get; set; }
        public Timeslot[] Timeslots { get; set; }
        public Tariff[] Tariffs { get; set; }
    }
}