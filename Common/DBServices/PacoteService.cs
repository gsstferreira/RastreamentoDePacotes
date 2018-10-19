using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;

namespace Common.DBServices
{
    public class PacoteService : IDisposable
    {
        private ISession session;

        public PacoteService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryPacote().OpenSession();
            return this;
        }
        public IEnumerable<Pacote> ObterTodosPacotes()
        {
            try
            {
                return session.QueryOver<Pacote>().List();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public IEnumerable<Pacote> ObterPorRemetente(Guid UsuarioId)
        {
            try
            {
                return session.QueryOver<Pacote>().Where(x => x.Remetente == UsuarioId).List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Pacote> ObterPorDestinatario(Guid UsuarioId)
        {
            try
            {
                return session.QueryOver<Pacote>().Where(x => x.DestinatarioId == UsuarioId).List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Pacote ObterPorId(Guid PacoteId)
        {
            try
            {
                return session.QueryOver<Pacote>().Where(x => x.PacoteId ==  PacoteId).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Pacote ObterPorTag(Guid TagRFID)
        {
            try
            {
                return session.QueryOver<Pacote>().Where(x => x.TagRFID.Equals(TagRFID)).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SalvarPacote(Pacote pacote)
        {
            try
            {
                session.SaveOrUpdate(pacote);
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
