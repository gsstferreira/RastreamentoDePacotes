using Common.DBServices;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRastreamento.Models
{
    public class RespostaRota
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Endereco Origem { get; set; }
        public Endereco Destino { get; set; }
        public ICollection<Localizacao> AmostrasLocalizacao { get; set; }

        public RespostaRota(Rota rota, EstacaoService estacaoService)
        {
            this.DataInicio = rota.DataInicio;
            this.DataFim = rota.DataFim;

            this.Origem = CopiarEndereco(estacaoService.ObterPorId(rota.Origem).Endereco);
            this.Destino = CopiarEndereco(estacaoService.ObterPorId(rota.Destino).Endereco);

            this.AmostrasLocalizacao = rota.AmostrasLocalizacao;
        }

        public static List<RespostaRota> ConverterRotas(IEnumerable<Rota> rotas, EstacaoService estacaoService)
        {
            var rots = new List<RespostaRota>();

            foreach(var r in rotas)
            {
                var rot = new RespostaRota(r, estacaoService);
                rots.Add(rot);
            }

            return rots;
        }

        private static Endereco CopiarEndereco(Endereco addr)
        {
            var e = new Endereco
            {
                EnderecoId = addr.EnderecoId,
                Bairro = addr.Bairro,
                Cep = addr.Cep,
                Complemento = addr.Complemento,
                Estado = addr.Estado,
                Latitude = addr.Latitude,
                Logradouro = addr.Logradouro,
                Longitude = addr.Longitude,
                Municipio = addr.Municipio,
                Numero = addr.Numero,
                Pais = addr.Pais
            };

            return e;
        }

    }
}