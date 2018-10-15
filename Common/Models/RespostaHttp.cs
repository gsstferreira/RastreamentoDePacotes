using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RespostaHttp
    {
        public virtual bool Ok { get; set; }
        public virtual dynamic Mensagem { get; set; }
    }
}
