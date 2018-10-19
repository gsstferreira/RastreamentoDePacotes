using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using System;
using System.Web.Mvc;

namespace APIRastreamento.Controllers
{
    public class LocalizacaoController : BaseApiController 
    {
        public LocalizacaoController():base()
        {
            _veiculoService.OpenSession();
            _rotaService.OpenSession();
            _localizacaoService.OpenSession();
        }

        [HttpPost]
        public string PostarLocalizacao()
        {
            RespostaHttp resp = new RespostaHttp();

            try
            {
                Guid VeiculoID = _requestBody.GetValueAs<Guid>("VeiculoId");
                var veiculo = _veiculoService.ObterPorId(VeiculoID);
                Rota rota = _rotaService.ObterPorId(veiculo.RotaAtual);

                var localizacao = new Localizacao
                {
                    HorarioAmostra = DateTime.UtcNow,
                    Latitude = _requestBody.GetValueAs<double>("Latitude"),
                    Longitude = _requestBody.GetValueAs<double>("Longitude"),
                    Rota = rota
                };

                _localizacaoService.SalvarLocalizacao(localizacao);

                resp.Ok = true;
                resp.Mensagem = "Localização salva com sucesso.";
            }
            catch (Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }
            return Serialize(resp);
        }
    }
}