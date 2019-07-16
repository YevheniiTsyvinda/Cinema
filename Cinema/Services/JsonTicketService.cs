using Cinema.Models.Tickets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Services
{
    public class JsonTicketService : ITicketService
    {
        private HttpContext Context { get; set; }
        private const string PathToJson = "~/Files/Tickets.json";

        public JsonTicketService()
        {
            Context = HttpContext.Current;
        }

        public Hall[] GetAllHalls()
        {
            var fullModel = GetDataFromFile();
            Hall[] halls = fullModel.Halls;
            return halls;
        }

        public Movie[] GetAllMovies()
        {
            var fullModel = GetDataFromFile();
            Movie[] movie = fullModel.Movies;
            return movie;
        }

        public Tariff[] GetAllTariffs()
        {

            var fullModel = GetDataFromFile();
            Tariff[] tariffs = fullModel.Tariffs;
            return tariffs;
        }

        public Timeslot[] GetAllTimeslots()
        {
            var fullModel = GetDataFromFile();
            Timeslot[] timeslots = fullModel.Timeslots;
            return timeslots;
        }

        public Hall GetHallById(int id)
        {
            var fullModel = GetDataFromFile();
            Hall hall = fullModel.Halls.FirstOrDefault(m => m.Id == id);
            return hall;
        }

        public Movie GetMovieById(int id)
        {
            var fullModel = GetDataFromFile();
            Movie movie = fullModel.Movies.FirstOrDefault(m => m.Id == id);
            return movie;
        }

        public Tariff GetTariffById(int id)
        {
            var fullModel = GetDataFromFile();
            Tariff tariff = fullModel.Tariffs.FirstOrDefault(m => m.Id == id);
            return tariff;
        }

        public Timeslot GetTimeslotById(int id)
        {
            var fullModel = GetDataFromFile();
            Timeslot timeslot = fullModel.Timeslots.FirstOrDefault(m => m.Id == id);
            return timeslot;
        }

        private TicketsJsonModel GetDataFromFile()
        {
            var jsonFilePath = Context.Server.MapPath(PathToJson);

            if (!System.IO.File.Exists(jsonFilePath))
            {
                return null;
            }

            var jsonModel = System.IO.File.ReadAllText(jsonFilePath);
            var deserializeModel = JsonConvert.DeserializeObject<TicketsJsonModel>(jsonModel);
            return deserializeModel;
        }

        public bool UpdateMovie(Movie updateMovie)
        {
            var fullModel = GetDataFromFile();
            var movieToUpdate = fullModel.Movies.FirstOrDefault(movie => movie.Id == updateMovie.Id);

            if (movieToUpdate == null)
                return false;

            movieToUpdate.Name = updateMovie.Name;
            movieToUpdate.Id = updateMovie.Id;
            movieToUpdate.ImageUrl = updateMovie.ImageUrl;
            movieToUpdate.MinAge = updateMovie.MinAge;
            movieToUpdate.Rating = updateMovie.Rating;
            if (updateMovie.Types != null)
            {
                movieToUpdate.Types = updateMovie.Types;
            }
            if (updateMovie.Genres != null)
            {
                movieToUpdate.Genres = updateMovie.Genres;
            }
            movieToUpdate.Duration = updateMovie.Duration;
            movieToUpdate.Description = updateMovie.Description;

            SaveDataToFile(fullModel);
            return true;
        }

        private void SaveDataToFile(TicketsJsonModel fullModel)
        {
            var jsonFilePath = Context.Server.MapPath(PathToJson);
            var serializedModel = JsonConvert.SerializeObject(fullModel);
            System.IO.File.WriteAllText(jsonFilePath, serializedModel);
        }

        public bool UpdateHall(Hall updateHall)
        {
            var fullModel = GetDataFromFile();
            var hallToUpdate = fullModel.Halls.FirstOrDefault(hall => hall.Id == updateHall.Id);

            if (hallToUpdate == null)
                return false;

            hallToUpdate.Id = updateHall.Id;
            hallToUpdate.Name = updateHall.Name;

            SaveDataToFile(fullModel);
            return true;
        }

        public bool UpdateTariff(Tariff updateTariff)
        {
            var fullModel = GetDataFromFile();
            var tariffToUpdate = fullModel.Tariffs.FirstOrDefault(tariff => tariff.Id == updateTariff.Id);

            if (tariffToUpdate == null)
                return false;

            tariffToUpdate.Id = updateTariff.Id;
            tariffToUpdate.Name = updateTariff.Name;
            tariffToUpdate.Cost = updateTariff.Cost;

            SaveDataToFile(fullModel);
            return true;
        }

        public bool UpdateTimeslot(Timeslot updateTimeslot)
        {
            var fullModel = GetDataFromFile();
            var timeslotToUpdate = fullModel.Timeslots.FirstOrDefault(timeslot => timeslot.Id == updateTimeslot.Id);

            if (timeslotToUpdate == null)
                return false;

            timeslotToUpdate.MovieId = updateTimeslot.MovieId;
            timeslotToUpdate.HallId = updateTimeslot.HallId;
            timeslotToUpdate.StartTime = updateTimeslot.StartTime;
            timeslotToUpdate.TariffId = updateTimeslot.TariffId;

            SaveDataToFile(fullModel);
            return true;
        }

        public bool CreateMovie(Movie newMovie)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newMovieId = fullModel.Movies.Max(movie => movie.Id) + 1;
                newMovie.Id = newMovieId;
                var existingMoviesList = fullModel.Movies.ToList();
                existingMoviesList.Add(newMovie);
                fullModel.Movies = existingMoviesList.ToArray();
                SaveDataToFile(fullModel);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CreateTimeslot(Timeslot newTimeslot)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newTimeslotId = fullModel.Timeslots.Max(x => x.Id) + 1;
                newTimeslot.Id = newTimeslotId;
                var existingTimeslotList = fullModel.Timeslots.ToList();
                existingTimeslotList.Add(newTimeslot);
                fullModel.Timeslots = existingTimeslotList.ToArray();
                SaveDataToFile(fullModel);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CreateHall(Hall createHall)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newHallId = fullModel.Halls.Max(x => x.Id) + 1;
                createHall.Id = newHallId;
                var existingHallList = fullModel.Halls.ToList();
                existingHallList.Add(createHall);
                fullModel.Halls = existingHallList.ToArray();
                SaveDataToFile(fullModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateTariff(Tariff createTariff)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newTariffId = fullModel.Tariffs.Max(x => x.Id) + 1;
                createTariff.Id = newTariffId;
                var existingTariffList = fullModel.Tariffs.ToList();
                existingTariffList.Add(createTariff);
                fullModel.Tariffs = existingTariffList.ToArray();
                SaveDataToFile(fullModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private Timeslot[] GetTimeslotsByMovieId(int movieId)
        {
            var timeslots = GetAllTimeslots().Where(x => x.MovieId == movieId).ToArray();
            return timeslots;
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            var movies = GetAllMovies();
            var resultModel = new List<MovieListItem>();
            foreach (var movie in movies)
            {
                resultModel.Add(new MovieListItem
                {
                    Movie = movie,
                    AvailableTimeslots = GetTimeslotTagsByMovieId(movie.Id)
                });
            }
            return resultModel.ToArray();
        }

        public TimeslotTag[] GetTimeslotTagsByMovieId(int movieId)
        {
            var timeslots = GetTimeslotsByMovieId(movieId);
            var tariffs = GetAllTariffs();
            var resultModel = new List<TimeslotTag>();

            foreach (var timeslot in timeslots)
            {
                resultModel.Add(new TimeslotTag {
                    TimeslotId = timeslot.Id,
                    StartTime = timeslot.StartTime,
                    Cost = tariffs.FirstOrDefault(x=>x.Id == timeslot.TariffId)?.Cost ?? 0
                });
            }
            return resultModel.ToArray();
        }

        public bool RemoveMovie(int movieId)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingMovie = fullModel.Movies.ToList();
                existingMovie.RemoveAll(x => x.Id == movieId);
                fullModel.Movies = existingMovie.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveHall(int hallId)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingHall = fullModel.Halls.ToList();
                existingHall.RemoveAll(x => x.Id == hallId);
                fullModel.Halls = existingHall.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveTariff(int tariffId)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingTariff = fullModel.Tariffs.ToList();
                existingTariff.RemoveAll(x => x.Id == tariffId);
                fullModel.Tariffs = existingTariff.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveTimeslot(int timeslotId)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var existingTimeslot = fullModel.Timeslots.ToList();
                existingTimeslot.RemoveAll(x => x.Id == timeslotId);
                fullModel.Timeslots = existingTimeslot.ToArray();
                SaveDataToFile(fullModel);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request)
        {
            var fullModel = GetDataFromFile();
            var timeslotToUpdate = fullModel.Timeslots.
                FirstOrDefault(timeslot => timeslot.Id == request.TimeslotId);
            if (timeslotToUpdate == null)
                return false;

            List<TimeslotSeatRequest> requestToProcess;
            if (timeslotToUpdate.RequestedSeats != null && timeslotToUpdate.RequestedSeats.Any())
            {
                requestToProcess = timeslotToUpdate.RequestedSeats.ToList();
            }
            else
            {
                requestToProcess = new List<TimeslotSeatRequest>();
            }

            if (request?.SeatsRequest?.AddedSeats == null || !request.SeatsRequest.AddedSeats.Any())
                return false;

            foreach (var addedSeats in request.SeatsRequest.AddedSeats)
            {
                requestToProcess.Add(new TimeslotSeatRequest
                {

                    Row = addedSeats.Row,
                    Seat = addedSeats.Seat,
                    Status = request.SelectedStatus
                });
            }
        
            timeslotToUpdate.RequestedSeats = requestToProcess.ToArray();
            SaveDataToFile(fullModel);
            return true;
            
        }

        public MovieListItem[] SerchMoviesByTerm(string term)
        {
            throw new NotImplementedException();
        }
    }
}