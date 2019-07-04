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

        public DbSet<GenreDTO> Genres { get; set; }
        public DbSet<TypeDTO> Types { get; set; }
        public DbSet<MovieDTO> Movies { get; set; }
        public DbSet<HallDTO> Halls { get; set; }
        public DbSet<TariffDTO> Tariffs { get; set; }
        public DbSet<RequestedSeatsDTO> RequestedSeats { get; set; }
        public DbSet<TimeslotDTO> Timeslots { get; set; }
    }
}