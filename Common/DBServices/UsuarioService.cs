using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class UsuarioService : IDisposable
    {
        private ISession session;

        public UsuarioService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryGeral().OpenSession();
            return this;
        }
        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            try
            {
                return session.QueryOver<Usuario>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuario ObterPorId(Guid UsuarioId)
        {
            try
            {
                return session.QueryOver<Usuario>().Where(x => x.UsuarioId == UsuarioId).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuario ObterPorEmail(string email)
        {
            try
            {
                return session.QueryOver<Usuario>().Where(x => x.Email == email).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SalvarUsuario(Usuario user)
        {
            try
            {
                session.SaveOrUpdate(user);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (session != null)
            {
                session.Close();
            }
        }
    }
}
