using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class EnderecoViewModel
    {

        public Guid EnderecoId { get; set; }

        [Required]
        [Display(Name ="Logradouro")]
        public string Logradouro { get; set; }

        [Required]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Required]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        [Required]
        [Display(Name = "EStado")]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "País")]
        public string Pais { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public static EnderecoViewModel FromEndereco(Endereco e)
        {
            var vm = new EnderecoViewModel
            {
                EnderecoId = e.EnderecoId,
                Logradouro = e.Logradouro,
                Numero = e.Numero,
                Complemento = e.Complemento,
                Cep = e.Cep,
                Bairro = e.Bairro,
                Municipio = e.Municipio,
                Estado = e.Estado,
                Pais = e.Pais,
                Latitude = e.Latitude,
                Longitude = e.Longitude
            };

            return vm;
        }

        public Endereco ToEndereco()
        {
            var e = new Endereco
            {
                EnderecoId = EnderecoId,
                Logradouro = Logradouro,
                Numero = Numero,
                Complemento = Complemento,
                Cep = Cep,
                Bairro = Bairro,
                Municipio = Municipio,
                Estado = Estado,
                Pais = Pais,
                Latitude = Latitude,
                Longitude = Longitude
            };

            return e;
        }
    }
}