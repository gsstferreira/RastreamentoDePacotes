using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mappings
{
    public class EmpresaMap : ClassMap<Empresa>
    {
        public EmpresaMap()
        {
            Id(x => x.EmpresaId).GeneratedBy.GuidComb();
            Map(x => x.Cnpj);
            Map(x => x.NomeEmpresa);
            Map(x => x.Senha);
            Map(x => x.DataCadastro);
        }
    }
}
