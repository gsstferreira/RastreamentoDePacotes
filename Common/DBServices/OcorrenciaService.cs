using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class OcorrenciaService : IDisposable
    {
        private ISession session;

        public OcorrenciaService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryGeral().OpenSession();
            return this;
        }

        public bool SalvarOcorrencia(Ocorrencia ocorrencia)
        {
            try
            {
                session.SaveOrUpdate(ocorrencia);
                session.Flush();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Ocorrencia> ObterPorPacote(Guid PacoteId)
        {
            try
            {
                return session.QueryOver<Ocorrencia>().Where(x => x.Pacote == PacoteId).List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Ocorrencia> ObterAtivasPorPacote(Guid PacoteId)
        {
            try
            {
                return session.QueryOver<Ocorrencia>().Where(x => x.Pacote == PacoteId).And(x => !x.Finalizado).List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Ocorrencia> ObterFinalizadasPorPacote(Guid PacoteId)
        {
            try
            {
                return session.QueryOver<Ocorrencia>().Where(x => x.Pacote == PacoteId).And(x => x.Finalizado).List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Ocorrencia ObterPorId(Guid OcorrenciaId)
        {
            try
            {
                return session.QueryOver<Ocorrencia>().Where(x => x.OcorrenciaId == OcorrenciaId).SingleOrDefault();
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
