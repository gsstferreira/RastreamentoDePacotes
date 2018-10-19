using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class LocalizacaoService : IDisposable
    {
        private ISession session;

        public LocalizacaoService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryPacote().OpenSession();
            return this;
        }
        public bool SalvarLocalizacao(Localizacao localizacao)
        {
            try
            {
                session.SaveOrUpdate(localizacao);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Localizacao> ObterTodasLocalizacoes()
        {
            try
            {
                return session.QueryOver<Localizacao>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Localizacao ObterPorId(Guid LocalizacaoId)
        {
            try
            {
                return session.QueryOver<Localizacao>().Where(x => x.LocalizacaoId.Equals(LocalizacaoId)).SingleOrDefault();
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
