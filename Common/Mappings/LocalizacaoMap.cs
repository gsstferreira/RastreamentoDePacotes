using Common.Models;
using FluentNHibernate.Mapping;

namespace Common.Mappings
{
    public class LocalizacaoMap : ClassMap<Localizacao>
    {
        public LocalizacaoMap()
        {
            Id(x => x.LocalizacaoId).GeneratedBy.GuidComb();

            Map(x => x.HorarioAmostra);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            References(x => x.Rota, "RotaId").Cascade.None();
        }
    }
}