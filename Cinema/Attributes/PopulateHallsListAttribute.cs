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
        public ITicketService TicketService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["HallsList"] = TicketService.GetAllHalls();

            base.OnActionExecuting(filterContext);
        }
    }
}