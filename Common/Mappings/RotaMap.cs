using Common.Models;
using FluentNHibernate.Mapping;

namespace Common.Mappings
{
    public class RotaMap : ClassMap<Rota>
    {
        public RotaMap()
        {
            Id(x => x.RotaId).GeneratedBy.GuidComb();
            Map(x => x.DataInicio);
            Map(x => x.DataFim);
            Map(x => x.VeiculoTransporte);
            Map(x => x.Origem);
            Map(x => x.Destino);

            HasManyToMany(x => x.Pacotes)
                .Table("RotasPacote")
                .ParentKeyColumn("RotaId")
                .ChildKeyColumn("PacoteId")
                .Not.LazyLoad()
                .Cascade.None();

            HasMany(x => x.AmostrasLocalizacao).KeyColumn("RotaId").Cascade.None();
        }
    }
}