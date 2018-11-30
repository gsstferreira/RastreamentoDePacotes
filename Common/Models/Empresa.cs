using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Empresa
    {
        public virtual Guid EmpresaId { get; set; }
        public virtual string NomeEmpresa { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string Senha { get; set; }
        public virtual DateTime DataCadastro { get; set; }
    }
}
