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

        public virtual bool VerificarSenha(string senha)
        {
            var enc1 = Convert.ToBase64String(Encoding.UTF32.GetBytes(senha));
            var enc2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(enc1));

            return Senha.Equals(enc2);
        }

        public virtual void CriarSenha(string senha)
        {
            var enc1 = Convert.ToBase64String(Encoding.UTF32.GetBytes(senha));
            var enc2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(enc1));

            Senha = enc2;
        }
    }
}
