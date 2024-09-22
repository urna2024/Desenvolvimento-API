using MapeiaVoto.Domain.Entidades;
using System.Text.Json.Serialization;

namespace MapeiaVoto.Application.Models
{
    public class NivelEscolaridadeModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        [JsonIgnore]
        public virtual ICollection<Entrevistado> entrevistado { get; set; } = new List<Entrevistado>();
    }
}
