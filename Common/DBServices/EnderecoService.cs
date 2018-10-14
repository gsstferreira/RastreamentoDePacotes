using Common.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DBServices
{
    public abstract class EnderecoService
    {
        public static IEnumerable<Endereco> ObterTodosEnderecos()
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var enderecos = session.QueryOver<Endereco>().List();
                session.Close();

                return enderecos;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Endereco ObterPorId(Guid EnderecoId)
        {
            try
            {
                ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                var endereco = session.QueryOver<Endereco>().Where(x => x.EnderecoId.Equals(EnderecoId)).SingleOrDefault();
                session.Close();

                return endereco;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool SalvarEndereco(Endereco endereco)
        {
            try
            {
                if(endereco.EnderecoId == null)
                {
                    var enderecoSimilar = ObterTodosEnderecos().Where(x => x.Latitude == endereco.Latitude)
                        .Where(x => x.Longitude == endereco.Longitude)
                        .Where(x => x.Complemento.Equals(endereco.Complemento)).FirstOrDefault();

                    if(enderecoSimilar != null)
                    {
                        endereco.EnderecoId = enderecoSimilar.EnderecoId;
                    }
                    else
                    {
                        ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                        session.SaveOrUpdate(endereco);
                        session.Close();
                    }
                    return true;

                }
                else
                {
                    ISession session = NHibernateFactory.CreateSessionGeral().OpenSession();
                    session.SaveOrUpdate(endereco);
                    session.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
