using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Item
    {
        public virtual string Descricao { get; set; }
        public virtual int Quantidade { get; set; }
    }
}
