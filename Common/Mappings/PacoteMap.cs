using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Mappings
{
    public class PacoteMap : ClassMap<Pacote>
    {
        public PacoteMap()
        {
            Id(x => x.PacoteId).GeneratedBy.GuidComb();
            Map(x => x.TagRFID);
            Map(x => x.DataPostagem);
            Map(x => x.Destinatario);
            Map(x => x.DestinatarioId);
            Map(x => x.Destino);
            Map(x => x.Entregue);
            Map(x => x.Remetente);

            HasManyToMany(x => x.Rotas)
                .Table("RotasPacote")
                .ParentKeyColumn("PacoteId")
                .ChildKeyColumn("RotaId")
                .Cascade.None();
        }
    }
}