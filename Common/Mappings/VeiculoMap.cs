using Common.Models;
using FluentNHibernate.Mapping;

namespace Common.Mappings
{
    public class VeiculoMap : ClassMap<Veiculo>
    {
        public VeiculoMap()
        {
            Id(x => x.VeiculoId).GeneratedBy.GuidComb();
            Map(x => x.RotaAtual);
            Map(x => x.Modelo);
            Map(x => x.Placa);
        }
    }
}