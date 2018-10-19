using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class EstacaoService : IDisposable
    {
        private ISession session;

        public EstacaoService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryGeral().OpenSession();
            return this;
        }

        public bool SalvarEstacao(Estacao Estacao)
        {
            try
            {
                session.SaveOrUpdate(Estacao);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Estacao> ObterTodasEstacoes()
        {
            try
            {
                return session.QueryOver<Estacao>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Estacao ObterPorId(Guid EstacaoId)
        {
            try
            {
                return session.QueryOver<Estacao>().Where(x => x.EstacaoId.Equals(EstacaoId)).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
            if(session != null)
            {
                session.Close();
            }
        }
    }
}
