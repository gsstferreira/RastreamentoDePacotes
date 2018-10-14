using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public abstract class UsuarioService
    {
        public static IEnumerable<Usuario> ObterTodosUsuarios()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var pacotes = session.QueryOver<Usuario>().List();
                session.Close();

                return pacotes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuario ObterPorId(Guid UsuarioId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var user = session.QueryOver<Usuario>().Where(x => x.UsuarioId.Equals(UsuarioId)).SingleOrDefault();
                session.Close();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuario ObterPorEmail(string email)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var user = session.QueryOver<Usuario>().Where(x => x.Email.Equals(email)).SingleOrDefault();
                session.Close();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool SalvarUsuario(Usuario user)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                session.SaveOrUpdate(user);
                session.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
