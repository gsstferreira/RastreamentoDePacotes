using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public abstract class EmpresaService
    {
        public static bool SalvarEmpresa(Empresa Empresa)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                session.SaveOrUpdate(Empresa);
                session.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<Empresa> ObterTodasEmpresas()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var empresas = session.QueryOver<Empresa>().List();
                session.Close();

                return empresas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Empresa ObterPorId(Guid EmpresaId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var empresa = session.QueryOver<Empresa>().Where(x => x.EmpresaId.Equals(EmpresaId)).SingleOrDefault();
                session.Close();

                return empresa;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
