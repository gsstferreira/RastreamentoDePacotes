using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Usuario
    {
        public virtual Guid UsuarioId { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Nome { get; set; }
    }
}
