using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappings
{
    public class OcorrenciaMap : ClassMap<Ocorrencia>
    {
        public OcorrenciaMap()
        {
            Id(x => x.OcorrenciaId).GeneratedBy.GuidComb();
            Map(x => x.Pacote);
            Map(x => x.TipoOCorrencia);
            Map(x => x.Descricao);
            Map(x => x.Data);
            Map(x => x.Resolvido);
            Map(x => x.Finalizado);
        }
    }
}
