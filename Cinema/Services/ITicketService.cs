using Cinema.Models.Tickets;

namespace Cinema.Services
{
    public interface ITicketService
    {
        Movie GetMovieById(int id);
        Movie[] GetAllMovies();
        bool UpdateMovie(Movie updateMovie);
        bool CreateMovie(Movie createMovie);
        bool RemoveMovie(int movieId);

        Hall GetHallById(int id);
        Hall[] GetAllHalls();
        bool UpdateHall(Hall updateHall);
        bool CreateHall(Hall createHall);
        bool RemoveHall(int hallId);


        Tariff GetTariffById(int id);
        Tariff[] GetAllTariffs();
        bool UpdateTariff(Tariff updateTariff);
        bool CreateTariff(Tariff createTariff);
        bool RemoveTariff(int tariffId);

        Timeslot GetTimeslotById(int id);
        Timeslot[] GetAllTimeslots();
        bool UpdateTimeslot(Timeslot updateTimeslot);
        bool CreateTimeslot(Timeslot createTimeslot);
        bool RemoveTimeslot(int timeslotid);

        MovieListItem[] GetFullMoviesInfo();

        TimeslotTag[] GetTimeslotTagsByMovieId(int movieId);

        bool AddRequestedSeatsToTimeslot(SeatsProcessRequest request);

        MovieListItem[] SerchMoviesByTerm(string term);
    }
}