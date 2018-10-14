using Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIRastreamento.Controllers
{
    public class HomeController : BaseController 
    {
        public ActionResult Index()
        {

            Guid g = AuthUser.UsuarioId;
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
