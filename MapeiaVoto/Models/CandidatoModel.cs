using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Application.Models
{
    public class CandidatoModel
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; }
        public string nomeUrna { get; set; }
        public DateTime dataNascimento { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public string foto { get; set; }
        public int idStatus { get; set; }
        public virtual Status status { get; set; }

        public int idPartidoPolitico { get; set; }
        public virtual PartidoPolitico partidopolitico { get; set; }

        public int idCargoDisputado { get; set; }
        public virtual CargoDisputado cargodisputado { get; set; }
    }
}
