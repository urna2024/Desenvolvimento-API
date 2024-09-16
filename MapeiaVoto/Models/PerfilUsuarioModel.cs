using MapeiaVoto.Domain.Entidades;
using System.Text.Json.Serialization;

namespace MapeiaVoto.Application.Models
{
    public class PerfilUsuarioModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        [JsonIgnore]
        public virtual ICollection<Usuario> usuario { get; set; } = new List<Usuario>();
    }
}
