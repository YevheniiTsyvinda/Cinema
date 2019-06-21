using Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Attributes
{
    public class PopulateMoviesListAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ticketService = new JsonTicketService(HttpContext.Current);

            filterContext.Controller.ViewData["MoviesList"] = ticketService.GetAllMovies();

            base.OnActionExecuting(filterContext);
        }
    }
}