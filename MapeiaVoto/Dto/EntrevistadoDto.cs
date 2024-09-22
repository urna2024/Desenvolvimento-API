using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Application.Dto
{
    public class EntrevistadoDto
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; }
        public DateTime dataNascimento { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public string? celular { get; set; }
        public int idGenero { get; set; }
        public int idNivelEscolaridade { get; set; }
        public int idRendaFamiliar { get; set; }
       
    }
}
