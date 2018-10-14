using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Localizacao
    {
        public virtual Guid LocalizacaoId { get; set; }
        public virtual Rota Rota { get; set; }
        public virtual DateTime HorarioAmostra { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }
}
