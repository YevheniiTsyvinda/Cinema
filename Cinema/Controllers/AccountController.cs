using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cinema.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            LogIn login = new LogIn();

            return View("~/Views/Login.cshtml",login);
        }
        [HttpPost]
        public ActionResult LogIn(LogIn logIn)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Views/Login.cshtml",logIn);
            }

            return View("~/Views/LoginResult.cshtml",logIn);
        }
    }
}