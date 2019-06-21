using Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Attributes
{
    public class PopulateHallsListAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ticketService = new JsonTicketService(HttpContext.Current);

            filterContext.Controller.ViewData["HallsList"] = ticketService.GetAllHalls();

            base.OnActionExecuting(filterContext);
        }
    }
}