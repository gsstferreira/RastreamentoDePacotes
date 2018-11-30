using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Ocorrencia
    {
        public virtual Guid OcorrenciaId { get; set; }
        public virtual Guid Pacote { get; set; }
        public virtual TipoOCorrencia TipoOCorrencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual bool Resolvido { get; set; }
        public virtual bool Finalizado { get; set; }
    }


    public enum TipoOCorrencia
    {
        Roubo,
        Extravio,
        NaoEncontrado,
        Danificado
    }
}
