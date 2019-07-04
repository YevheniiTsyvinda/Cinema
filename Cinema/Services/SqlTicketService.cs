using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Cinema.Models.Tickets;
using Cinema.Utils;

namespace Cinema.Services
{
    public class SqlTicketService : ITicketService
    {
        private SqlDataBaseUtil DataBaseUtil { get; set; }

        private IMapper Mapper { get; set; }

        public SqlTicketService(IMapper mapper)
        {
            DataBaseUtil = new SqlDataBaseUtil(mapper);
            
        }
        public bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request)
        {
            throw new NotImplementedException();
        }

        public bool CreateHall(Hall createHall)
        {
            throw new NotImplementedException();
        }

        public bool CreateMovie(Movie createMovie)
        {
            throw new NotImplementedException();
        }

        public bool CreateTariff(Tariff createTariff)
        {
            throw new NotImplementedException();
        }

        public bool CreateTimeslot(Timeslot createTimeslot)
        {
            throw new NotImplementedException();
        }

        public Hall[] GetAllHalls()
        {
            throw new NotImplementedException();
        }

        public Movie[] GetAllMovies()
        {
            return  DataBaseUtil.Execute<Movie>("SelectAllMovies").ToArray();

        }

        public Tariff[] GetAllTariffs()
        {
            throw new NotImplementedException();
        }

        public Timeslot[] GetAllTimeslots()
        {
            throw new NotImplementedException();
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            throw new NotImplementedException();
        }

        public Hall GetHallById(int id)
        {
            throw new NotImplementedException();
        }

        public Movie GetMovieById(int id)
        {
            throw new NotImplementedException();
        }

        public Tariff GetTariffById(int id)
        {
            throw new NotImplementedException();
        }

        public Timeslot GetTimeslotById(int id)
        {
            throw new NotImplementedException();
        }

        public TimeslotTag[] GetTimeslotTagsByMovieId(int movieId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHall(int hallId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveMovie(int movieId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTariff(int tariffId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTimeslot(int timeslotid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHall(Hall updateHall)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMovie(Movie updateMovie)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTariff(Tariff updateTariff)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTimeslot(Timeslot updateTimeslot)
        {
            throw new NotImplementedException();
        }
    }
}