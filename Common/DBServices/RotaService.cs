using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;

namespace Common.DBServices
{
    public class RotaService : IDisposable
    {
        private ISession session;

        public RotaService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryPacote().OpenSession();
            return this;
        }

        public IEnumerable<Rota> ObterTodasRotas()
        {
            try
            {
                return session.QueryOver<Rota>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SalvarRota(Rota rota)
        {
            try
            {
                session.SaveOrUpdate(rota);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Rota ObterPorId(Guid RotaId)
        {
            try
            {
                return session.QueryOver<Rota>().Where(x => x.RotaId == RotaId).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
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
