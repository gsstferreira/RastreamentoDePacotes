using Common.DBServices;
using System;
using System.Web.Mvc;

namespace AplicacaoArduino.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            PacoteService.ObterTodosPacotes();
            UsuarioService.ObterTodosUsuarios();

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
