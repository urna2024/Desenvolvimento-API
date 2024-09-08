using MapeiaVoto.Domain.Entidades;
using System.Text.Json.Serialization;

namespace MapeiaVoto.Application.Models
{
    public class PartidoPoliticoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        [JsonIgnore]
        public virtual ICollection<Candidato> candidato { get; set; } = new List<Candidato>();
    }
}
