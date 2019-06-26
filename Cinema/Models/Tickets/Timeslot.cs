using System;

namespace Cinema.Models.Tickets
{
    public class Timeslot
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public int TariffId { get; set; }
        public TimeslotSeatRequest[] RequestedSeats { get; set; }
    }
}