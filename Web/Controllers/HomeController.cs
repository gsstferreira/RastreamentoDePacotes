using Common.DBServices;
using System;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Pacotes()
        {
            return View();
        }

        public ActionResult Rotas(Guid PacoteId)
        {
            return View();
        }

        public ActionResult Localizacoes(Guid RotaId)
        {
            return View();
        }
    }
}
