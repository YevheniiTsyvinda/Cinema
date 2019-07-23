using AutoMapper;
using Cinema.Attributes;
using Cinema.Binders;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Managers;
using Cinema.Models.Reports;
using Cinema.Models.Tickets;
using Cinema.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;


namespace Cinema.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketService _ticketSetvice;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;


        public TicketsController(ITicketService ticketService,IMapper mapper, ICacheManager cache)
        {
            _ticketSetvice = ticketService;
            _mapper = mapper;
            _cache = cache;
        }

        public ActionResult GetMovies()
        {
            var cacheKey = "Tickets_GetMovies";
            var allMovies = _cache.CacheResult(() => _ticketSetvice.GetFullMoviesInfo(),cacheKey); //кеширование
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

        [HttpGet]
        public ActionResult SearchForFilms()
        {
            return View("~/Views/Tickets/Search/SearchForm.cshtml");
        }

        [HttpPost]
        public string SearchForFilms(string term, int currentPage = 1, int pageSize = 1)
        {
            var cacheKey = string.Format("Tickets_SearchForFilms_Term:{0}_CurrentPage:{1}_PageSize:{2}", term, currentPage, pageSize);
            

            var cacheResult = _cache.CacheResult(() =>
           {
               var allResults = _ticketSetvice.SerchMoviesByTerm(term);
               var totalPages = Math.Ceiling(allResults.Length / (double)pageSize);
               var curentPageResult = allResults.Skip((currentPage - 1) * pageSize).Take(pageSize).ToArray();
               var model = new SearchFilmResult
               {
                   Result = curentPageResult,
                   TotalPages = (int)totalPages,
                   CurrentPage = currentPage,
                   ShowPaging = totalPages > 1
               };
               var resultModel = JsonConvert.SerializeObject(model);
               return resultModel;
           },
           cacheKey);

            return cacheResult;
        }

        [HttpGet]
        public ActionResult Reports()
        {
            return View("~/Views/Tickets/Reports/Reports.cshtml");
        }
        [HttpGet]
        public ActionResult GetReportForm(ReportType type)
        {
            var currentView = ReportsFactory.GetReportFormView(type);
            return PartialView(currentView);
        }

        [HttpPost]
        public ActionResult BuildReport([ModelBinder(typeof(BaseReportFormModelBinder))]BaseReportForm form)//modelbinder используется для получения нужной модели
        {
            var reportStrategy = ReportsFactory.GetReportStrategy(form, _mapper);
            var reportLink = reportStrategy.BuildReport();
            return View("~/Views/Tickets/Reports/DownloadReports.cshtml",model:reportLink);
        }
    }
}