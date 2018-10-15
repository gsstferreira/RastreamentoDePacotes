using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using System;
using System.Web.Mvc;

namespace APIRastreamento.Controllers
{
    public class LocalizacaoController : BaseController 
    {
        [HttpPost]
        public string PostarLocalizacao()
        {
            RespostaHttp resp = new RespostaHttp();

            var body = RequestReaderService.ReadRequestBody(Request);

            try
            {
                Guid VeiculoID = body.GetValueAs<Guid>("VeiculoId");
                var veiculo = VeiculoService.ObterPorId(VeiculoID);
                Rota rota = RotaService.ObterPorId(veiculo.RotaAtual);

                var localizacao = new Localizacao
                {
                    HorarioAmostra = DateTime.UtcNow,
                    Latitude = body.GetValueAs<double>("Latitude"),
                    Longitude = body.GetValueAs<double>("Longitude"),
                    Rota = rota
                };

                LocalizacaoService.SalvarLocalizacao(localizacao);

                resp.Ok = true;
                resp.Mensagem = "Localização salva com sucesso.";
            }
            catch (Exception)
            {
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }
            return serializer.Serialize(resp);
        }
    }
}