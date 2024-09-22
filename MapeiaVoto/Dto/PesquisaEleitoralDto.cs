using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Application.Dto
{
    public class PesquisaEleitoralDto
    {
        public int id { get; set; }
        public DateTime dataEntrevista { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public bool votoIndeciso { get; set; }
        public bool votoBrancoNulo { get; set; }
        public string sugestaoMelhoria { get; set; }
        // Relacionamento com os candidatos
        public int? idCandidatoPrefeito { get; set; } // Prefeito escolhido
        public int? idCandidatoVereador { get; set; } // Vereador escolhido                                   
        public int idUsuario { get; set; }
        public int idStatus { get; set; }
        public List<EntrevistadoDto> entrevistado { get; set; } = new List<EntrevistadoDto>();

    }
}
