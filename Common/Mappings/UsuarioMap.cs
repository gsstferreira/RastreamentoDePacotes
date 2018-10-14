using Common.Models;
using FluentNHibernate.Mapping;

namespace Common.Mappings
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(x => x.UsuarioId).GeneratedBy.GuidComb();
            Map(x => x.Email);
            Map(x => x.Nome);
            Map(x => x.Senha);
            Map(x => x.DataCadastro);

        }
    }
}