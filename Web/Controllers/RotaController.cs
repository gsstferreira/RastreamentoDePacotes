using Common.Controllers;
using Common.DBServices;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;
using static Web.ViewModels.RotaViewModel;

namespace Web.Controllers
{
    public class RotaController : BaseWebController
    {

        public RotaController():base()
        {
            _pacoteService.OpenSession();
            _rotaService.OpenSession();
            _estacaoService.OpenSession();
            _veiculoService.OpenSession();
        }

        public ActionResult Index()
        {
            var rotas = _rotaService.ObterTodasRotas();
            var rotasVm = new List<RotaViewModel>();

            foreach(var r in rotas)
            {
                var v = _veiculoService.ObterPorId(r.VeiculoTransporte);

                RotaViewModel vm = new RotaViewModel
                {
                    RotaId = r.RotaId,
                    DataInicio = r.DataInicio,
                    DataFim = r.DataFim,
                    AmostrasLocalizacao = r.AmostrasLocalizacao,
                    AmostrasVetor = latlng.converterLista(r.AmostrasLocalizacao.ToList()),
                    Origem = new EstacaoViewModel(_estacaoService.ObterPorId(r.Origem)),
                    Destino = new EstacaoViewModel(_estacaoService.ObterPorId(r.Destino)),
                    Veiculo = new VeiculoViewModel
                    {
                        VeiculoId = v.VeiculoId,
                        Modelo = v.Modelo,
                        Placa = v.Placa
                    }
                };

                rotasVm.Add(vm);
            }

            return View(rotasVm);
        }

        public ActionResult Detalhes(Guid rota)
        {

            var r = _rotaService.ObterPorId(rota);
            var v = _veiculoService.ObterPorId(r.VeiculoTransporte);

            RotaViewModel vm = new RotaViewModel
            {
                RotaId = r.RotaId,
                DataInicio = r.DataInicio,
                DataFim = r.DataFim,
                AmostrasLocalizacao = r.AmostrasLocalizacao,
                AmostrasVetor = latlng.converterLista(r.AmostrasLocalizacao.ToList()),
                Origem = new EstacaoViewModel(_estacaoService.ObterPorId(r.Origem)),
                Destino = new EstacaoViewModel(_estacaoService.ObterPorId(r.Destino)),
                Veiculo = new VeiculoViewModel
                {
                    VeiculoId = v.VeiculoId,
                    Modelo = v.Modelo,
                    Placa = v.Placa
                }
            };

            return View(vm);
        }

        public ActionResult Reset(Guid rotaId)
        {
            var rota = _rotaService.ObterPorId(rotaId);

            if(rota != null)
            {
                rota.Pacotes.Clear();
                rota.AmostrasLocalizacao.Clear();
                rota.DataInicio = DateTime.UtcNow;
                rota.DataFim = DateTime.MinValue;

                _rotaService.SalvarRota(rota);
            }

            var veiculo = _veiculoService.ObterPorId(rota.VeiculoTransporte);

            veiculo.RotaAtual = rota.RotaId;
            _veiculoService.SalvarVeiculo(veiculo);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var estacoes = _estacaoService.ObterTodasEstacoes();
            var veiculos = _veiculoService.ObterTodosVeiculos();
            var listaEstacoes = new List<SelectListItem>();
            var listaVeiculos = new List<SelectListItem>();

            foreach (var e in estacoes)
            {
                var addr = e.Endereco;
                SelectListItem s = new SelectListItem
                {
                    Text = string.Format("{0} {1}, {2} - {3}", addr.Logradouro, addr.Numero, addr.Bairro, addr.Municipio),
                    Value = e.EstacaoId.ToString()
                };
                listaEstacoes.Add(s);
            }

            foreach (var v in veiculos)
            {
                SelectListItem s = new SelectListItem
                {
                    Text = string.Format("{0} - {1}", v.Modelo,v.Placa),
                    Value = v.VeiculoId.ToString()
                };
                listaVeiculos.Add(s);
            }

            var novaRota = new NovaRotaViewModel
            {
                Estacoes = listaEstacoes,
                Veiculos = listaVeiculos
            };

            return View(novaRota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NovaRotaViewModel rota)
        {
            if (ModelState.IsValid)
            {

                Rota r = new Rota
                {
                    AmostrasLocalizacao = new List<Localizacao>(),
                    DataInicio = DateTime.UtcNow,
                    Origem = rota.Origem,
                    Destino = rota.Destino,
                    VeiculoTransporte = rota.Veiculo,
                    Pacotes = new List<Pacote>(),
                };

                _rotaService.SalvarRota(r);

                return RedirectToAction("Index");
            }
            else
            {
                return View(rota);
            }
        }
    }

}