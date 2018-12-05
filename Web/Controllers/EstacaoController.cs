using Common.Controllers;
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
    public class EstacaoController : BaseWebController
    {

        public EstacaoController() : base()
        {
            _estacaoService.OpenSession();
            _enderecoService.OpenSession();
        }

        // GET: Estacao
        public ActionResult Index()
        {
            var estacoes = _estacaoService.ObterTodasEstacoes();

            var lista = new List<EstacaoViewModel>();

            foreach(var item in estacoes)
            {
                var vm = new EstacaoViewModel(item);

                lista.Add(vm);

            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View(new EnderecoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnderecoViewModel end)
        {
            if (ModelState.IsValid)
            {
                var addr = end.ToEndereco();

                var latlng = GeocodingService.obterCoordenadas(addr);
                addr.Latitude = latlng.Latitude;
                addr.Longitude = latlng.Longitude;

                var addrId = _enderecoService.SalvarEndereco(addr);

                var estacao = new Estacao
                {
                    Endereco = _enderecoService.ObterPorId(addrId),
                    Latitude = latlng.Latitude,
                    Longitude = latlng.Longitude
                };

                _estacaoService.SalvarEstacao(estacao);

                return RedirectToAction("Index");

            }
            else
            {
                return View(end);
            }
        }
    }
}