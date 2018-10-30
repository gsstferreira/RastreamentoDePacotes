using Newtonsoft.Json;
using System;

namespace Common.Models
{
    public class Localizacao
    {
        [JsonIgnore]
        public virtual Guid LocalizacaoId { get; set; }
        [JsonIgnore]
        public virtual Rota Rota { get; set; }
        public virtual DateTime HorarioAmostra { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }
}
