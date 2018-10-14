using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;

namespace Common.DBServices
{
    public abstract class RotaService
    {
        public static IEnumerable<Rota> ObterTodasRotas()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var itens = session.QueryOver<Rota>().List();
                session.Close();

                return itens;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool SalvarRota(Rota rota)
        {
            try
            {
                ISession sessionPacote = NHibernateFactory.CreateSessionPacote().OpenSession();
                sessionPacote.SaveOrUpdate(rota);
                sessionPacote.Close();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Rota ObterPorId(Guid RotaId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var rota = session.QueryOver<Rota>().Where(x => x.RotaId.Equals(RotaId)).SingleOrDefault();

                session.Close();

                return rota;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
