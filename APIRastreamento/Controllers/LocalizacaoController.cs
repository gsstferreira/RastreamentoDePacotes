using Common.Controllers;
using Common.DBServices;
using Common.Models;
using Common.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            _estacaoService.OpenSession();
        }

        [HttpPost]
        [AllowAnonymous]
        public string PostarLocalizacao()
        {
            RespostaHttp resp = new RespostaHttp();

            try
            {
                Guid VeiculoID = _requestBody.GetValueAs<Guid>("VeiculoId");
                List<string> Tags = _requestBody.GetValueAs<List<string>>("Tags");

                double lat = _requestBody.GetValueAs<double>("Latitude");
                double lng = _requestBody.GetValueAs<double>("Longitude");

                var veiculo = _veiculoService.ObterPorId(VeiculoID);
                Rota rota = _rotaService.ObterPorId(veiculo.RotaAtual);

                resp = LogicaPost(Tags, lat, lng, rota);

                if (resp == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                resp = new RespostaHttp();
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }
            return Serialize(resp);
        }


        [HttpGet]
        [AllowAnonymous]
        public string PostarPorQuery()
        {
            RespostaHttp resp = new RespostaHttp();

            try
            {
                var query = Request.QueryString;

                if (query.Count != 4)
                {
                    resp.Ok = false;
                    resp.Mensagem = "O formato da requisição está incorreto. (400)";
                }

                else
                {
                    Guid VeiculoId = new Guid(query.Get("V"));
                    List<string> Tags = JsonConvert.DeserializeObject<List<string>>(query.Get("T"));
                    double lat = Double.Parse(query.Get("lat"),CultureInfo.InvariantCulture);
                    double lng = Double.Parse(query.Get("lng"), CultureInfo.InvariantCulture);


                    var veiculo = _veiculoService.ObterPorId(VeiculoId);
                    var rota = _rotaService.ObterPorId(veiculo.RotaAtual);

                    resp = LogicaPost(Tags, lat, lng, rota);

                    if(resp == null)
                    {
                        throw new Exception();
                    }

                }

            }
            catch (Exception)
            {
                resp = new RespostaHttp();
                resp.Ok = false;
                resp.Mensagem = "Ocorreu um erro ao processar a requisição. (500)";
            }
            return Serialize(resp);
        }


        private RespostaHttp LogicaPost(List<string> Tags, double latitude, double longitude, Rota rota)
        {
            var resp = new RespostaHttp();
            
            if(rota.DataFim.CompareTo(rota.DataInicio) > 0)
            {
                resp.Ok = false;
                resp.Mensagem = "Essa rota já foi finalizada e não aceita novas amostras.";
                return resp;
            }
            else
            {
                try
                {
                    var leituraPacotes = new List<Pacote>();

                    foreach (var s in Tags)
                    {
                        Pacote p = _pacoteService.ObterPorTag(s);

                        if (p != null)
                        {
                            leituraPacotes.Add(p);
                        }
                    }

                    if (rota.AmostrasLocalizacao.Count == 0)
                    {
                        rota.Pacotes = leituraPacotes;
                        _rotaService.SalvarRota(rota);
                    }

                    else
                    {
                        foreach (var item in rota.Pacotes)
                        {
                            if (!leituraPacotes.Exists(x => x.PacoteId == item.PacoteId))
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
                        Latitude = latitude,
                        Longitude = longitude,
                        Rota = rota
                    };

                    _localizacaoService.SalvarLocalizacao(localizacao);

                    var estacaoDestino = _estacaoService.ObterPorId(rota.Destino);
                    var addr = estacaoDestino.Endereco;

                    double distancia = GeocodingService.Haversine(latitude, longitude, addr.Latitude, addr.Longitude);

                    if (distancia <= 20)
                    {
                        rota.DataFim = DateTime.UtcNow;
                        _rotaService.SalvarRota(rota);

                        resp.Ok = true;
                        resp.Mensagem = "Localização salva com sucesso, rota finalizada.";

                        return resp;

                    }
                    else
                    {
                        resp.Ok = true;
                        resp.Mensagem = "Localização salva com sucesso.";

                        return resp;
                    }
                }

                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}