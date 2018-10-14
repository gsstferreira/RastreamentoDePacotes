using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Endereco
    {
        public virtual Guid EnderecoId { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Cep { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Municipio { get; set; }
        public virtual string Estado { get; set; }
        public virtual string Pais { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }
}
