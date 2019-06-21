using Cinema.Attributes;
using Cinema.Models.Tickets;
using Cinema.Services;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;


namespace Cinema.Controllers
{
    public class TicketsAdminController : Controller
    {

        private readonly ITicketService _ticketSetvice;

        public TicketsAdminController()
        {
            _ticketSetvice = new JsonTicketService(System.Web.HttpContext.Current);
        }

        public ActionResult FindMovieById(int id)
        {
            var movie = _ticketSetvice.GetMovieById(id);
            if (movie == null)
                return Content("Movie with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(movie);
            return Content(modelJson, "application/json");
        }
        public ActionResult FindHallById(int id)
        {
            var hall = _ticketSetvice.GetHallById(id);
            if (hall == null)
                return Content("Hall with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(hall);
            return Content(modelJson, "application/json");
        }
        public ActionResult FindTariffById(int id)
        {
            var tariff = _ticketSetvice.GetTariffById(id);
            if (tariff == null)
                return Content("Tariff with such id do not exist");

            var modelJson = JsonConvert.SerializeObject(tariff);
            return Content(modelJson, "application/json");
        }

        public ActionResult GetMoviesList()
        {
            var movies = _ticketSetvice.GetAllMovies();
            return View("~/Views/TicketsAdmin/MoviesList.cshtml", movies);
        }
        [HttpGet]
        public ActionResult EditMovie(int movieId)
        {
            var movie = _ticketSetvice.GetMovieById(movieId);
            return View("~/Views/TicketsAdmin/EditMovie.cshtml", movie);
        }
        [HttpPost]
        public ActionResult EditMovie(Movie updateMovie)
        {
            var updateResult = _ticketSetvice.UpdateMovie(updateMovie);
            if(updateResult)
            {
                return RedirectToAction("GetMoviesList");
            }
            return Content("Update failed. Pleace, contact system administrator");
        }
        [HttpGet]
        public ActionResult AddMovie()
        {
            return View("~/Views/TicketsAdmin/AddMovie.cshtml");
        }
        [HttpPost]
        public ActionResult AddMovie(Movie newMovie)
        {
            var creationResult = _ticketSetvice.CreateMovie(newMovie);
            if (creationResult)
            {
                return RedirectToAction("GetMoviesList");
            }
            return Content("Update failed. Pleace, contact system administrator");
        }

        public ActionResult GetHallsList()
        {
            var halls = _ticketSetvice.GetAllHalls();
            return View("~/Views/TicketsAdmin/HallsList.cshtml", halls);
        }
        [HttpGet]
        public ActionResult EditHall(int hallId)
        {
            var hall = _ticketSetvice.GetHallById(hallId);
            return View("~/Views/TicketsAdmin/EditHall.cshtml", hall);
        }
        [HttpPost]
        public ActionResult EditHall(Hall updateHall)
        {
            var updateResult = _ticketSetvice.UpdateHall(updateHall);
            if (updateResult)
            {
                return RedirectToAction("GetHallsList");
            }
            return Content("Update failed. Pleace, contact system administrator");
        }
        public ActionResult GetTariffsList()
        {
            var tariffs = _ticketSetvice.GetAllTariffs();
            return View("~/Views/TicketsAdmin/TariffsList.cshtml", tariffs);
        }
        [HttpGet]
        public ActionResult EditTariff(int tariffId)
        {
            var tariff = _ticketSetvice.GetTariffById(tariffId);
            return View("~/Views/TicketsAdmin/EditTariff.cshtml", tariff);
        }
        [HttpPost]
        public ActionResult EditTariff(Tariff updateTariff)
        {
            var updateResult = _ticketSetvice.UpdateTariff(updateTariff);
            if (updateResult)
            {
                return RedirectToAction("GetTariffsList");
            }
            return Content("Update failed. Pleace, contact system administrator");
        }
        public ActionResult GetTimeslotsList()
        {
            var timeslots = _ticketSetvice.GetAllTimeslots();
            return View("~/Views/TicketsAdmin/TimeslotsList.cshtml", timeslots);
        }
        [HttpGet]
        [PopulateMoviesList,PopulateHallsList,PopulateTariffsList]
        public ActionResult EditTimeslot(int timesloteId)
        {
            var timeslote = _ticketSetvice.GetTimeslotById(timesloteId);
            return View("~/Views/TicketsAdmin/EditTimeslot.cshtml", timeslote);
        }
        [HttpPost]
        public ActionResult EditTimeslot(Timeslot updateTimeslot)
        {
            var updateResult = _ticketSetvice.UpdateTimeslot(updateTimeslot);
            if (updateResult)
            {
                return RedirectToAction("GetTimeslotsList");
            }
            return Content("Update failed. Pleace, contact system administrator");
        }
        [HttpGet]
        [PopulateMoviesList, PopulateHallsList, PopulateTariffsList]
        public ActionResult AddTimeslot()
        {
            return View("~/Views/TicketsAdmin/AddTimeslot.cshtml");
        }
        [HttpPost]
        public ActionResult AddTimeslot(Timeslot newTimeslot)
        {
            var creationResult = _ticketSetvice.CreateTimeslot(newTimeslot);
            if (creationResult)
            {
                return RedirectToAction("GetTimeslotsList");
            }
            return Content("Update failed. Pleace, contact system administrator");
        }
        public ActionResult GetMovieTimeslotsList(int movieId)
        {
            var timeslots = _ticketSetvice.GetAllTimeslots();
            timeslots = timeslots.Where(x => x.MovieId == movieId).ToArray();
            return View("~/Views/TicketsAdmin/TimeslotsList.cshtml", timeslots);
        }

    }
}