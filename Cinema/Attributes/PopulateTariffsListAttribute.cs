using Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Attributes
{
    public class PopulateTariffsListAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ticketService = new JsonTicketService(HttpContext.Current);

            filterContext.Controller.ViewData["TariffsList"] = ticketService.GetAllTariffs();

            base.OnActionExecuting(filterContext);
        }
    }
}