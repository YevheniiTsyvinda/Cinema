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
        public ITicketService TicketService { get; set; }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            filterContext.Controller.ViewData["MoviesList"] = TicketService.GetAllMovies();

            base.OnActionExecuting(filterContext);
        }
    }
}