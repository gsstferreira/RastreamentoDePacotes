using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class VeiculoService : IDisposable
    {
        private ISession session;

        public VeiculoService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryGeral().OpenSession();
            return this;
        }
        public bool SalvarVeiculo(Veiculo Veiculo)
        {
            try
            {
                session.SaveOrUpdate(Veiculo);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Veiculo> ObterTodosVeiculos()
        {
            try
            {
                return session.QueryOver<Veiculo>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Veiculo ObterPorId(Guid VeiculoId)
        {
            try
            {
                return session.QueryOver<Veiculo>().Where(x => x.VeiculoId == VeiculoId).SingleOrDefault();
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
