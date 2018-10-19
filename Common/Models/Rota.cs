using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class Rota
    {
        public virtual Guid RotaId { get; set; }
        public virtual Guid VeiculoTransporte { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual Guid Origem { get; set; }
        public virtual Guid Destino { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pacote> Pacotes { get; set; }
        public virtual ICollection<Localizacao> AmostrasLocalizacao { get; set; }
    }
}
