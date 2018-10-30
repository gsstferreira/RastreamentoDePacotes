using Common.DBServices;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRastreamento.Models
{
    public class RespostaPacote
    {
        public Guid PacoteId { get; set; }
        public bool Entregue { get; set; }
        public string Destinatario { get; set; }
        public string Remetente { get; set; } 
        public IEnumerable<RespostaRota> Rotas { get; set; }
        public IEnumerable<Item> Conteudo { get; set; }
        public DateTime DataPostagem { get; set; }
        public Endereco Destino { get; set; }


        public RespostaPacote(Pacote pacote, string NomeEmpresa, bool manterRotas, EstacaoService estacaoService,EnderecoService enderecoService)
        {
            this.PacoteId = pacote.PacoteId;
            this.Entregue = pacote.Entregue;
            this.Destinatario = pacote.Destinatario;
            this.DataPostagem = pacote.DataPostagem;
            this.Destino = enderecoService.ObterPorId(pacote.Destino);
            this.Remetente = NomeEmpresa;

            if(manterRotas)
            {
                this.Rotas = RespostaRota.ConverterRotas(pacote.Rotas,estacaoService);
                this.Conteudo = pacote.Conteudo;
            }
            else
            {
                this.Rotas = new List<RespostaRota>();
                this.Conteudo = new List<Item>();
            }
        }

        public static List<RespostaPacote> ConverterLista(ICollection<Pacote> pacotes, EnderecoService enderecoService, EmpresaService empresaService, EstacaoService estacaoService, bool manterRotas)
        {
            var pacs = new List<RespostaPacote>();

            foreach(var p in pacotes)
            {
                var empresa = empresaService.ObterPorId(p.Remetente);

                var respPac = new RespostaPacote(p,empresa.NomeEmpresa,manterRotas,estacaoService,enderecoService);

                pacs.Add(respPac);
            }

            return pacs;
        }

    }
}