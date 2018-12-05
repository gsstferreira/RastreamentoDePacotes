using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PacoteController : BaseWebController
    {


        public PacoteController():base()
        {
            _pacoteService.OpenSession();
            _empresaService.OpenSession();
            _enderecoService.OpenSession();
        }

        // GET: Pacote
        public ActionResult Index()
        {
            var pacotes = _pacoteService.ObterTodosPacotes();

            var pacotesViewModel = new List<PacoteViewModel>();

            foreach(var item in pacotes)
            {
                var empresa = _empresaService.ObterPorId(item.Remetente);
                var endereco = _enderecoService.ObterPorId(item.Destino);

                var pvm = new PacoteViewModel
                {
                    PacoteId = item.PacoteId,
                    DataPostagem = item.DataPostagem,
                    Destinatario = item.Destinatario,
                    Remetente = empresa.NomeEmpresa,
                    Destino = EnderecoViewModel.FromEndereco(endereco),
                    TagRfid = item.TagRFID,
                    Entregue = item.Entregue
                };

                pacotesViewModel.Add(pvm);
            }

            return View(pacotesViewModel);
        }

        public ActionResult Create()
        {
            PacoteViewModel p = new PacoteViewModel
            {
                Destinatario = "",
                Destino = new EnderecoViewModel(),
                Remetente = "",
                TagRfid = "",
            };

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PacoteViewModel pacote)
        {
            if(ModelState.IsValid)
            {
                Pacote p = new Pacote
                {
                    TagRFID = pacote.TagRfid,
                    DataPostagem = DateTime.UtcNow,
                    Destinatario = pacote.Destinatario,
                    DestinatarioId = Guid.Empty,
                    Codigo = "",
                    Entregue = false,
                    Rotas = new List<Rota>(),
                    Conteudo = new List<Item>(),
                    Remetente = new Guid("ef849f5864534e3cadeba07a3cd9de94"),
                };

                if (pacote.Destino.Complemento == null)
                {
                    pacote.Destino.Complemento = "";
                }

                var addr = pacote.Destino.ToEndereco();

                var latlng = GeocodingService.obterCoordenadas(addr);
                addr.Latitude = latlng.Latitude;
                addr.Longitude = latlng.Longitude;

                var addrId = _enderecoService.SalvarEndereco(addr);

                p.Destino = addrId;

                _pacoteService.SalvarPacote(p);

                return RedirectToAction("Index");
                
            }
            else
            {
                return View(pacote);
            }
        }
    }
}