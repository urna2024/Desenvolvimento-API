using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Application.Models
{
    public class EntrevistadoModel
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; }
        public DateTime dataNascimento { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public string? celular { get; set; }
        public int idGenero { get; set; }
        public virtual Genero genero { get; set; }
        public int idNivelEscolaridade { get; set; }
        public virtual NivelEscolaridade nivelescolaridade { get; set; }
        public int idRendaFamiliar { get; set; }
        public virtual RendaFamiliar rendafamiliar { get; set; }
        public int? idPesquisaEleitoralMunicipal { get; set; } // Chave estrangeira
        public virtual PesquisaEleitoralMunicipal pesquisaeleitoralmunicipal { get; set; } // Propriedade de navegação
    }
}
