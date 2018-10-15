using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;

namespace Common.DBServices
{
    public abstract class PacoteService
    {
        public static IEnumerable<Pacote> ObterTodosPacotes()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var pacotes = session.QueryOver<Pacote>().List();
                session.Close();

                return pacotes;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static IEnumerable<Pacote> ObterPorRemetente(Guid UsuarioId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var pacotes = session.QueryOver<Pacote>().Where(x => x.Remetente == UsuarioId).List();
                session.Close();

                return pacotes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<Pacote> ObterPorDestinatario(Guid UsuarioId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var pacotes = session.QueryOver<Pacote>().Where(x => x.DestinatarioId == UsuarioId).List();
                session.Close();

                return pacotes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Pacote ObterPorId(Guid PacoteId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var pacote = session.QueryOver<Pacote>().Where(x => x.PacoteId ==  PacoteId).SingleOrDefault();
                session.Close();

                return pacote;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Pacote ObterPorTag(Guid TagRFID)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var pacote = session.QueryOver<Pacote>().Where(x => x.TagRFID.Equals(TagRFID)).SingleOrDefault();
                session.Close();

                return pacote;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool SalvarPacote(Pacote pacote)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();

                session.SaveOrUpdate(pacote);
                session.Flush();
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
