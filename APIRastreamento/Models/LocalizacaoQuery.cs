using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRastreamento.Models
{
    public class LocalizacaoQuery
    {
        public List<string> Tags { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid VeiculoId { get; set; }

        public LocalizacaoQuery ParseQuery(string queryString)
        {
            try
            {
                var r = new LocalizacaoQuery();

                IEnumerable<string> valores = queryString.Split('&');

                bool foundT = false;
                bool foundV = false;
                bool foundLat = false;
                bool foundLng = false;

                foreach(string valor in valores)
                {
                    if (valor.StartsWith("T=") && !foundT)
                    {
                        r.Tags = JsonConvert.DeserializeObject<List<string>>(valor.Replace("T=",""));
                    }

                    else if (valor.StartsWith("V=") && !foundV)
                    {
                        r.VeiculoId = new Guid(valor.Replace("V=", ""));
                    }

                    else if (valor.StartsWith("lat=") && !foundLat)
                    {
                        r.Latitude = Double.Parse(valor.Replace("lat=", ""));
                    }

                    else if (valor.StartsWith("lng=") && !foundLng)
                    {
                        r.Longitude = Double.Parse(valor.Replace("lng=", ""));
                    }
                    else
                    {
                        return null;
                    }
                }

                if(!foundT || !foundV || !foundLat || !foundLng)
                {
                    return null;
                }

                else
                {
                    return r;
                }
            }
            catch(Exception)
            {
                return null;
            } 
        }
    }
}