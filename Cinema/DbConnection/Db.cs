using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Cinema.DTO;

namespace Cinema.DbConnection
{
    public class Db:DbContext
    {
        public Db():base("CinemaDbConnection")
        { }

        DbSet<GenreDTO> Genres { get; set; }
        DbSet<TypeDTO> Types { get; set; }
        DbSet<MovieDTO> Movies { get; set; }
        DbSet<HallDTO> Halls { get; set; }
        DbSet<TariffDTO> Tariffs { get; set; }
        DbSet<RequestedSeatsDTO> RequestedSeats { get; set; }
        DbSet<TimeslotDTO> Timeslots { get; set; }
    }
}