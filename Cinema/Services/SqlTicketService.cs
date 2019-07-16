using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


        public SqlTicketService(IMapper mapper)
        {
            DataBaseUtil = new SqlDataBaseUtil(mapper);
            
        }
        public bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request)
        {
            //создаём таблицу и колонки для работы с SeatsProcessRequest т.к. в базе такой таблицы нет
            var requeestTable = new DataTable("TimeslotSeatRequest");
            requeestTable.Columns.Add("Row", typeof(int));
            requeestTable.Columns.Add("Seat", typeof(int));
            requeestTable.Columns.Add("Status", typeof(int));

            foreach (var seatRequest in request.SeatsRequest.AddedSeats)
            {
                requeestTable.Rows.Add(seatRequest.Row, seatRequest.Seat, request.SelectedStatus);
            }

            SqlParameter requestTableparameter = new SqlParameter
            {
                ParameterName = "seatsRequest",
                SqlDbType = SqlDbType.Structured,
                Value = requeestTable
            };

            var parameters = new[]{
                requestTableparameter,
               new SqlParameter("@TimeslotId",request.TimeslotId)
            };

            return DataBaseUtil.ExecuteNonQuery("AddRequestedSeatsToTimeslot", parameters) != 0;
        }

        public bool CreateHall(Hall createHall)
        {
            var parameters = new[]
             {
                new SqlParameter("@Name",createHall.Name)
            };
            return DataBaseUtil.ExecuteNonQuery("AddHall", parameters) != 0;
        }

        public bool CreateMovie(Movie createMovie)
        {
            var parameters = new[]
              {
                new SqlParameter("@Name",createMovie.Name),
                new SqlParameter("@Description",createMovie.Description),
                new SqlParameter("@Duration",createMovie.Duration),
                new SqlParameter("@Rating",createMovie.Rating),
                new SqlParameter("@MinAge",createMovie.MinAge),
                new SqlParameter("@Types",createMovie.Types!= null && createMovie.Types.Any()? string.Join(",",createMovie.Types):string.Empty),
                new SqlParameter("@Genres",createMovie.Genres!= null && createMovie.Genres.Any()? string.Join(",",createMovie.Genres):string.Empty),
                new SqlParameter("@ImageUrl",createMovie.ImageUrl)
            };

            return DataBaseUtil.ExecuteNonQuery("AddMovie", parameters) != 0;
        }

        public bool CreateTariff(Tariff createTariff)
        {
            var parameters = new[]
             {
                new SqlParameter("@Name",createTariff.Name),
                new SqlParameter("@Cost",createTariff.Cost)
            };

            return DataBaseUtil.ExecuteNonQuery("AddTariff", parameters) != 0;
        }

        public bool CreateTimeslot(Timeslot createTimeslot)
        {
            var parameters = new[]
            {
                new SqlParameter("@StartTime",createTimeslot.StartTime),
                new SqlParameter("@MoviesId",createTimeslot.MovieId),
                new SqlParameter("@HallId",createTimeslot.HallId),
                new SqlParameter("@TariffId",createTimeslot.TariffId)
            };

            return DataBaseUtil.ExecuteNonQuery("AddTimeslot", parameters) != 0;
        }

        public Hall[] GetAllHalls()
        {
            return DataBaseUtil.Execute<Hall>("SelectAllHalls").ToArray();
        }

        public Movie[] GetAllMovies()
        {
            return  DataBaseUtil.Execute<Movie>("SelectAllMovies").ToArray(); 
        }

        public Tariff[] GetAllTariffs()
        {
            return DataBaseUtil.Execute<Tariff>("SelectAllTariffs").ToArray();
        }

        public Timeslot[] GetAllTimeslots()
        {
            return DataBaseUtil.Execute<Timeslot>("SelectAllTimeslots").ToArray();
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            return DataBaseUtil.Execute("GetFullMoviesInfo", null, _movieListItemMappingFunc).ToArray();
        }

        public Hall GetHallById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id",id)
            };

            return DataBaseUtil.Execute<Hall>("SelectHallById", parameters).FirstOrDefault();
        }

        public Movie GetMovieById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id",id)
            };

            return DataBaseUtil.Execute<Movie>("SelectMovieById",parameters).FirstOrDefault();
        }

        public Tariff GetTariffById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id",id)
            };

            return DataBaseUtil.Execute<Tariff>("SelectTariffById", parameters).FirstOrDefault();
        }

        public Timeslot GetTimeslotById(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id",id)
            };

            Func<SqlDataReader, List<Timeslot>, List<Timeslot>> mappingFunc = (reader, rawTimeslots) =>
              {
                  var processedColection = new List<Timeslot>();
                  processedColection.AddRange(rawTimeslots);
                  if (reader.NextResult())
                  {
                      while (reader.Read())
                      {
                          var targetTimeslot = processedColection.FirstOrDefault(x => x.Id == (int)reader["TimeslotId"]);
                          if (targetTimeslot == null)
                              continue;

                          var targetTimeslotRequestedSeatsList = targetTimeslot.RequestedSeats.ToList();
                          targetTimeslotRequestedSeatsList.Add(Mapper.Map<TimeslotSeatRequest>(reader));
                          targetTimeslot.RequestedSeats = targetTimeslotRequestedSeatsList.ToArray();
                      }
                  }
                  return processedColection;
              };

            return DataBaseUtil.Execute<Timeslot>("SelectTimeslotById", parameters,mappingFunc).FirstOrDefault();
        }

        public TimeslotTag[] GetTimeslotTagsByMovieId(int movieId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", movieId)
            };

            return DataBaseUtil.Execute<TimeslotTag>("SelectTimeslotTagsByMovieId", parameters).ToArray();
        }

        public bool RemoveHall(int hallId)
        {
            var parameters = new[]
             {
                new SqlParameter("@Id", hallId),

            };

            return DataBaseUtil.ExecuteNonQuery("DeleteHall", parameters) != 0;
        }

        public bool RemoveMovie(int movieId)
        {
            var parameters = new[]
             {
                new SqlParameter("@Id",movieId),
               
            };

            return DataBaseUtil.ExecuteNonQuery("DeleteMovie", parameters) != 0;
        }

        public bool RemoveTariff(int tariffId)
        {
            var parameters = new[]
             {
                new SqlParameter("@Id",tariffId),

            };

            return DataBaseUtil.ExecuteNonQuery("DeleteTariff", parameters) != 0;
        }

        public bool RemoveTimeslot(int timeslotid)
        {
            var parameters = new[]
              {
                new SqlParameter("@Id",timeslotid),

            };

            return DataBaseUtil.ExecuteNonQuery("DeleteTimeslot", parameters) != 0;
        }

        public bool UpdateHall(Hall updateHall)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id",updateHall.Id),
                new SqlParameter("@Name",updateHall.Name)
            };

            return DataBaseUtil.ExecuteNonQuery("UpdateHall", parameters) != 0;
        }

        public bool UpdateMovie(Movie updateMovie)
        {
            var parameters = new[]
             {
                new SqlParameter("@Id",updateMovie.Id),
                new SqlParameter("@Name",updateMovie.Name),
                new SqlParameter("@Description",updateMovie.Description),
                new SqlParameter("@Duration",updateMovie.Duration),
                new SqlParameter("@Rating",updateMovie.Rating),
                new SqlParameter("@MinAge",updateMovie.MinAge),
                new SqlParameter("@Types",updateMovie.Types!= null && updateMovie.Types.Any()? string.Join(",",updateMovie.Types):string.Empty),
                new SqlParameter("@Genres",updateMovie.Genres!= null && updateMovie.Genres.Any()? string.Join(",",updateMovie.Genres):string.Empty),
                new SqlParameter("@ImageUrl",updateMovie.ImageUrl)
            };

            return DataBaseUtil.ExecuteNonQuery("UpdateMovie", parameters)!=0;
        }

        public bool UpdateTariff(Tariff updateTariff)
        {
            var parameters = new[]
             {
                new SqlParameter("@Id",updateTariff.Id),
                new SqlParameter("@Name",updateTariff.Name),
                new SqlParameter("@Cost",updateTariff.Cost)
            };
            return DataBaseUtil.ExecuteNonQuery("UpdateTaruff", parameters) != 0;
        }

        public bool UpdateTimeslot(Timeslot updateTimeslot)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id",updateTimeslot.Id),
                new SqlParameter("@StartTime",updateTimeslot.StartTime),
                new SqlParameter("@MoviesId",updateTimeslot.MovieId),
                new SqlParameter("@HallId",updateTimeslot.HallId),
                new SqlParameter("@TariffId",updateTimeslot.TariffId)
            };
            return DataBaseUtil.ExecuteNonQuery("UpdateTariff", parameters) != 0;
        }

        //делегат добавлен для возможности смапить данные второй возвращаемой таблицы по процедуре GetFullMoviesInfo
        private readonly Func<SqlDataReader, List<MovieListItem>, List<MovieListItem>> _movieListItemMappingFunc = (reader, rawMovieList) =>
          {
              var processedCollection = new List<MovieListItem>();
              processedCollection.AddRange(rawMovieList);
              if (reader.NextResult())
              {
                  while (reader.Read())
                  {
                      var targetMovie = processedCollection.FirstOrDefault(x => x.Movie.Id == (int)reader["MovieId"]);
                      if (targetMovie == null)
                          continue;
                      var targetMovieTimeslotsLists = targetMovie.AvailableTimeslots.ToList();
                      targetMovieTimeslotsLists.Add(Mapper.Map<TimeslotTag>(reader));
                      targetMovie.AvailableTimeslots = targetMovieTimeslotsLists.ToArray();
                  }
              }
              return processedCollection;
          };
        public MovieListItem[] SerchMoviesByTerm(string term)
        {
            var parameters = new[]
            {
                new SqlParameter("@Term",term)
            };
            return DataBaseUtil.Execute("GetFullMovieByTerm", parameters,_movieListItemMappingFunc).ToArray();
        }
    }
}