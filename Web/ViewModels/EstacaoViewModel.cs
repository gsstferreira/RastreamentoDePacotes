using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class EstacaoViewModel
    {
        public Guid EstacaoId { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public EstacaoViewModel(Estacao e)
        {
            EstacaoId = e.EstacaoId;
            Endereco = EnderecoViewModel.FromEndereco(e.Endereco);
        }

        public EstacaoViewModel() { }
    }
}