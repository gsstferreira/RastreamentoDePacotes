using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class EmpresaService : IDisposable
    {
        private ISession session;

        public EmpresaService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryGeral().OpenSession();
            return this;
        }

        public bool SalvarEmpresa(Empresa Empresa)
        {
            try
            {
                session.SaveOrUpdate(Empresa);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Empresa> ObterTodasEmpresas()
        {
            try
            {
                return session.QueryOver<Empresa>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Empresa ObterPorId(Guid EmpresaId)
        {
            try
            {
                return session.QueryOver<Empresa>().Where(x => x.EmpresaId.Equals(EmpresaId)).SingleOrDefault();
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
