﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class SeatsProcessRequest
    {
        public int TimeslotId { get; set; }

        public SeatsRequest SeatsRequest { get; set; }

        public RequestStatus SelectedStatus { get; set; }
    }
}