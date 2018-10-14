using Common.Models;
using FluentNHibernate.Mapping;

namespace Common.Mappings
{
    public class EstacaoMap : ClassMap<Estacao>
    {
        public EstacaoMap()
        {
            Id(x => x.EstacaoId).GeneratedBy.GuidComb();

            Map(x => x.Latitude);
            Map(x => x.Longitude);
            References(x => x.Endereco, "EnderecoId").Cascade.None();
        }
    }
}