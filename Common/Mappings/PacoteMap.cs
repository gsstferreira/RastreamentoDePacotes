using Common.Models;
using FluentNHibernate.Mapping;

namespace Common.Mappings
{
    public class PacoteMap : ClassMap<Pacote>
    {
        public PacoteMap()
        {
            Id(x => x.PacoteId).GeneratedBy.GuidComb();
            Map(x => x.TagRFID);
            Map(x => x.Codigo);
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
                .Inverse()
                .Cascade.None();

            HasMany(x => x.Conteudo)
                .Table("ConteudoPacote")
                .KeyColumn("PacoteId")
                .Component(y =>
                {
                    y.Map(z => z.Descricao);
                    y.Map(z => z.Quantidade);
                })
                .Cascade.None();
        }
    }
}