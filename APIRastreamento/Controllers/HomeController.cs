using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIRastreamento.Controllers
{
    public class HomeController : Controller 
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
