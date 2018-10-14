using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Veiculo
    {
        public virtual Guid VeiculoId { get; set; }
        public virtual Guid RotaAtual { get; set; }
    }
}
