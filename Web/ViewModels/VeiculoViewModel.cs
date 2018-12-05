using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VeiculoViewModel
    {
        public Guid VeiculoId { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}