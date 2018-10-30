using APIRastreamento.Models;
using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIRastreamento.Controllers
{
    public class PacoteController : BaseApiController
    {

        public PacoteController() : base()
        {
            _pacoteService.OpenSession();
            _empresaService.OpenSession();
            _enderecoService.OpenSession();
            _estacaoService.OpenSession();
        }

        [HttpGet]
        public string ObterPacotesAtivos()
        {
            var resp = new RespostaHttp();

            try
            {
                var pacotes = _pacoteService.ObterPorDestinatario(_user.UsuarioId).Where(x => !x.Entregue).ToList();

                var lista = RespostaPacote.ConverterLista(pacotes, _enderecoService, _empresaService, _estacaoService, false);

                resp.Ok = true;
                resp.Mensagem = lista;

            }
            catch(Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }

        [HttpGet]
        public string ObterPacotesHistorico()
        {
            var resp = new RespostaHttp();

            try
            {
                var pacotes = _pacoteService.ObterPorDestinatario(_user.UsuarioId).Where(x => x.Entregue).ToList();

                var lista = RespostaPacote.ConverterLista(pacotes, _enderecoService, _empresaService, _estacaoService, false);

                resp.Ok = true;
                resp.Mensagem = lista;

            }
            catch (Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }
        
        public string ObterDetalhesPacote()
        {
            var resp = new RespostaHttp();

            try
            {
                Guid pacoteId = _requestBody.GetValueAs<Guid>("PacoteId");

                var pacote = _pacoteService.ObterPorDestinatario(_user.UsuarioId).Where(x => x.PacoteId == pacoteId).First();

                var addr = _enderecoService.ObterPorId(pacote.Destino);
                var empresa = _empresaService.ObterPorId(pacote.Remetente);

                var respostaPacote = new RespostaPacote(pacote, empresa.NomeEmpresa, true, _estacaoService, _enderecoService);

                resp.Ok = true;
                resp.Mensagem = respostaPacote;

            }
            catch (Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }

        [HttpPost]
        public string RegistrarPacoteEmUsuario()
        {
            RespostaHttp resp = new RespostaHttp();

            try
            {
                Guid pacoteId = _requestBody.GetValueAs<string>("CodigoPacote");

                var pacote = _pacoteService.ObterPorId(pacoteId);

                if(pacote != null)
                {
                    if(pacote.DestinatarioId == Guid.Empty)
                    {
                        pacote.DestinatarioId = _user.UsuarioId;

                        _pacoteService.SalvarPacote(pacote);

                        resp.Ok = true;
                        resp.Mensagem = "Pacote associado com sucesso. (200)";
                    }
                    else
                    {
                        resp.Ok = false;
                        resp.Mensagem = "O pacote especificado já está associado a um outro usuário.";
                    }
                }
                else
                {
                    resp.Ok = false;
                    resp.Mensagem = "O pacote especificado não existe. (404)";
                }
            }
            catch(Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return Serialize(resp);
        }
    }
}