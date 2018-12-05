using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class RotaViewModel
    {
        public Guid RotaId { get; set; }

        public VeiculoViewModel Veiculo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataInicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataFim { get; set; }

        public EstacaoViewModel Origem { get; set; }

        public EstacaoViewModel Destino { get; set; }
        public IEnumerable<Localizacao> AmostrasLocalizacao { get; set; }

        public List<latlng> AmostrasVetor { get; set; } 


        public class latlng
        {
            public double lat { get; set; }
            public double lng { get; set; }

            public static List<latlng> converterLista(List<Localizacao> amostras)
            {
                var ret = new List<latlng>();

                amostras.Sort(delegate (Localizacao a, Localizacao b) 
                {
                    return a.HorarioAmostra.CompareTo(b.HorarioAmostra);
                });

                for(int i = 0; i < amostras.Count(); i++)
                {
                    var x = new latlng
                    {
                        lat = amostras.ElementAt(i).Latitude,
                        lng = amostras.ElementAt(i).Longitude
                    };

                    ret.Add(x);
                }

                return ret;
            }
        }
    }
}