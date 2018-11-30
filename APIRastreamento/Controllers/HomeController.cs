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

            var pacserv = new PacoteService().OpenSession();

            var pac = pacserv.ObterTodosPacotes().FirstOrDefault();

            var item1 = new Item
            {
                Descricao = "Kit Arduino Uno R3 + Leitor RFID Micfire",
                Quantidade = 1
            };

            var item2 = new Item
            {
                Descricao = "Conjunto Etiquetas RFID Alta Frequência 20 un.",
                Quantidade = 3
            };

            pac.Conteudo.Add(item1);
            pac.Conteudo.Add(item2);

            pacserv.SalvarPacote(pac);
            pacserv.Dispose();

            return View();
        }
    }
}
