using Cinema.Models.Tickets;

namespace Cinema.Services
{
    public interface ITicketService
    {
        Movie GetMovieById(int id);
        Movie[] GetAllMovies();
        bool UpdateMovie(Movie updateMovie);
        bool CreateMovie(Movie createMovie);

        Hall GetHallById(int id);
        Hall[] GetAllHalls();
        bool UpdateHall(Hall updateHall);

        Tariff GetTariffById(int id);
        Tariff[] GetAllTariffs();
        bool UpdateTariff(Tariff updateTariff);

        Timeslot GetTimeslotById(int id);
        Timeslot[] GetAllTimeslots();
        bool UpdateTimeslot(Timeslot updateTimeslot);
        bool CreateTimeslot(Timeslot createTimeslot);

    }
}