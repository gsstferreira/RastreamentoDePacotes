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
    public class PacoteController : BaseController
    {
        [HttpGet]
        public string ObterPacotesAtivos()
        {
            var resp = new RespostaHttp();
            var user = CheckUser(Request);

            try
            {
                var pacotes = PacoteService.ObterPorDestinatario(user.UsuarioId).Where(x => !x.Entregue).ToList();

                resp.Ok = true;
                resp.Mensagem = pacotes;

            }
            catch(Exception)
            {

                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return serializer.Serialize(resp);
        }

        [HttpGet]
        public string ObterPacotesHistorico()
        {

            var resp = new RespostaHttp();
            var user = CheckUser(Request);

            try
            {
                var pacotes = PacoteService.ObterPorDestinatario(user.UsuarioId).Where(x => x.Entregue);

                resp.Ok = true;
                resp.Mensagem = pacotes;
            }
            catch (Exception)
            {

                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }

            return serializer.Serialize(resp);
        }

        [HttpPost]
        public string RegistrarPacoteEmUsuario()
        {
            RespostaHttp resp = new RespostaHttp();

            var user = CheckUser(Request);
            var obj = RequestReaderService.ReadRequestBody(Request);

            try
            {
                Guid pacoteId = obj.GetValueAs<Guid>("PacoteId");

                var pacote = PacoteService.ObterPorId(pacoteId);

                if(pacote != null)
                {
                    if(pacote.DestinatarioId == Guid.Empty)
                    {
                        pacote.DestinatarioId = user.UsuarioId;

                        PacoteService.SalvarPacote(pacote);

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

            return serializer.Serialize(resp);
        }


        // Métodos Auxiliares
        private Usuario CheckUser(HttpRequestBase request)
        {
            Usuario user;

            try
            {
                Guid id = new Guid(request.Headers.Get("Authorization"));

                user = UsuarioService.ObterPorId(id);
            }
            catch(Exception)
            {
                user = null;
            }

            if(user != null)
            {
                return user;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}