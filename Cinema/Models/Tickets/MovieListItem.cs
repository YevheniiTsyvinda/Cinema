using Cinema.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class MovieListItem
    {
        public MovieListItem()
        {
            AvailableTimeslots = new TimeslotTag[0];
        }

        public Movie Movie { get; set; }

        public TimeslotTag[] AvailableTimeslots { get; set; }
    }
}