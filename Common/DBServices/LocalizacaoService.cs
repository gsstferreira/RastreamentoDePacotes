using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public abstract class LocalizacaoService
    {
        public static bool SalvarLocalizacao(Localizacao localizacao)
        {
            try
            {

                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                session.SaveOrUpdate(localizacao);
                session.Flush();
                session.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<Localizacao> ObterTodasLocalizacoes()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var localizacoes = session.QueryOver<Localizacao>().List();
                session.Close();

                return localizacoes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Localizacao ObterPorId(Guid LocalizacaoId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionPacote().OpenSession();
                var item = session.QueryOver<Localizacao>().Where(x => x.LocalizacaoId.Equals(LocalizacaoId)).SingleOrDefault();
                session.Close();

                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
