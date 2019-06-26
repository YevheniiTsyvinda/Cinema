using Cinema.Attributes;
using Cinema.Models.Tickets;
using Cinema.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Cinema.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketService _ticketSetvice;

        public TicketsController(ITicketService ticketService)
        {
            _ticketSetvice = ticketService;
        }

        public ActionResult GetMovies()
        {
            var allMovies = _ticketSetvice.GetFullMoviesInfo();
            return View("~/Views/Tickets/MoviesList.cshtml", allMovies);
        }
        public ActionResult GetHallInfo(int timeslotId)
        {
            var timeslot = _ticketSetvice.GetTimeslotById(timeslotId);
            var curentTariff = _ticketSetvice.GetTariffById(timeslot.TariffId);
            var model = new HallInfo
            {
                CurrentTariff = curentTariff,
                CurrentTimeslotId = timeslotId,
                RequestedSeats = timeslot.RequestedSeats
            };
            return View("~/Views/Tickets/HallInfo.cshtml",model);
        }
        public string ProcessRequest(SeatsProcessRequest request)
        {
            var requestProcessingResult = _ticketSetvice.AddRequestedSeatsToTimeslot(request);
            return JsonConvert.SerializeObject(new
            {
                requestResult = requestProcessingResult
            });
        }
    }
}