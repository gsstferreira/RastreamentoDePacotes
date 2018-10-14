using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public abstract class EstacaoService
    {
        public static bool SalvarEstacao(Estacao Estacao)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                session.SaveOrUpdate(Estacao);
                session.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<Estacao> ObterTodasEstacoes()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var itens = session.QueryOver<Estacao>().List();
                session.Close();

                return itens;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Estacao ObterPorId(Guid EstacaoId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var item = session.QueryOver<Estacao>().Where(x => x.EstacaoId.Equals(EstacaoId)).SingleOrDefault();
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
