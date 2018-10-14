using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Pacote
    {
        public virtual Guid PacoteId { get; set; }
        public virtual string TagRFID { get; set; }
        public virtual Guid Remetente { get; set; }
        public virtual Guid DestinatarioId { get; set; }
        public virtual string Destinatario { get; set; }
        public virtual ICollection<Rota> Rotas { get; set; }
        public virtual DateTime DataPostagem { get; set; }
        public virtual Guid Destino { get; set; }
        public virtual bool Entregue { get; set; }
    }
}
