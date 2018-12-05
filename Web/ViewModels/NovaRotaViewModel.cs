using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.ViewModels
{
    public class NovaRotaViewModel
    {
        public IEnumerable<SelectListItem> Estacoes { get; set; }
        public IEnumerable<SelectListItem> Veiculos { get; set; }

        [Required]
        [Display(Name = "Estação de Origem")]
        public Guid Origem { get; set; }

        [Required]
        [Display(Name = "Estação de Destino")]
        public Guid Destino { get; set; }

        [Required]
        [Display(Name = "Veículo")]
        public Guid Veiculo { get; set; }

        [Required]
        [Display(Name = "Data de Início")]
        public DateTime DataInicio { get; set; }
    }
}