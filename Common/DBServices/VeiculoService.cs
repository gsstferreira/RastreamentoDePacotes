using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public abstract class VeiculoService
    {
        public static bool SalvarVeiculo(Veiculo Veiculo)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                session.SaveOrUpdate(Veiculo);
                session.Flush();
                session.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<Veiculo> ObterTodosVeiculos()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var itens = session.QueryOver<Veiculo>().List();
                session.Close();

                return itens;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Veiculo ObterPorId(Guid VeiculoId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var item = session.QueryOver<Veiculo>().Where(x => x.VeiculoId == VeiculoId).SingleOrDefault();
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
