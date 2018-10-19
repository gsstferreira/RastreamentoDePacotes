using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public class EnderecoService : IDisposable
    {
        private ISession session;
        public EnderecoService OpenSession()
        {
            session = NHibernateFactory.GetSessionFactoryGeral().OpenSession();
            return this;
        }

        public IEnumerable<Endereco> ObterTodosEnderecos()
        {
            try
            {
                return session.QueryOver<Endereco>().List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Endereco ObterPorId(Guid EnderecoId)
        {
            try
            {
                return session.QueryOver<Endereco>().Where(x => x.EnderecoId == EnderecoId).SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Guid SalvarEndereco(Endereco endereco)
        {
            try
            {
                if(endereco.EnderecoId == Guid.Empty)
                {
                    var enderecoSimilar = ObterTodosEnderecos().Where(x => x.Latitude == endereco.Latitude)
                        .Where(x => x.Longitude == endereco.Longitude)
                        .Where(x => x.Complemento.Equals(endereco.Complemento)).FirstOrDefault();

                    if(enderecoSimilar != null)
                    {
                        return enderecoSimilar.EnderecoId;
                    }
                    else
                    {
                        session.SaveOrUpdate(endereco);
                        session.Flush();

                        return endereco.EnderecoId;
                    }
                }
                else
                {
                    session.SaveOrUpdate(endereco);

                    return endereco.EnderecoId;
                }
            }
            catch (Exception)
            {
                return Guid.Empty;
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
