namespace MapeiaVoto.Application.Dto
{
    public class CandidadoDto
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; }
        public string nomeUrna { get; set; }
        public DateTime dataNascimento { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public string foto { get; set; }
        public int idStatus { get; set; }
        public int idPartidoPolitico { get; set; }
        public int idCargoDisputado { get; set; }
    }
}
