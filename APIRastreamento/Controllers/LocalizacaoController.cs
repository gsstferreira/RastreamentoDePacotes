using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace APIRastreamento.Controllers
{
    public class LocalizacaoController : BaseApiController 
    {
        public LocalizacaoController():base()
        {
            _veiculoService.OpenSession();
            _pacoteService.OpenSession();
            _rotaService.OpenSession();
            _localizacaoService.OpenSession();
            _ocorrenciaService.OpenSession();
        }

        [HttpPost]
        public string PostarLocalizacao()
        {
            RespostaHttp resp = new RespostaHttp();

            try
            {
                Guid VeiculoID = _requestBody.GetValueAs<Guid>("VeiculoId");
                List<string> IdPacotes = _requestBody.GetValueAs<List<string>>("Tags");
                var veiculo = _veiculoService.ObterPorId(VeiculoID);
                Rota rota = _rotaService.ObterPorId(veiculo.RotaAtual);

                var leituraPacotes = new List<Pacote>();

                foreach (var s in IdPacotes)
                {
                    Pacote p = _pacoteService.ObterPorTag(s);

                    if (p != null)
                    {
                        leituraPacotes.Add(p);
                    }
                }

                if(rota.AmostrasLocalizacao.Count == 0)
                {
                    rota.Pacotes = leituraPacotes;
                    _rotaService.SalvarRota(rota);
                }

                else
                {
                    foreach(var item in rota.Pacotes)
                    {
                        if(!leituraPacotes.Contains(item))
                        {
                            Ocorrencia o = new Ocorrencia
                            {
                                Data = DateTime.UtcNow,
                                Descricao = "Pacote não encontrado no veículo",
                                Pacote = item.PacoteId,
                                TipoOCorrencia = TipoOCorrencia.NaoEncontrado,
                                Finalizado = false,
                                Resolvido = false
                            };

                            _ocorrenciaService.SalvarOcorrencia(o);
                        }
                    }
                }

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