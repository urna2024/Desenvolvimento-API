using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class PesquisaEleitoralMunicipal : BaseEntity
    {
        public DateTime dataEntrevista { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public bool votoIndeciso { get; set; }
        public bool votoBrancoNulo { get; set; }
        public string sugestaoMelhoria { get; set; }
        // Relacionamento com os candidatos
        public int? idCandidatoPrefeito { get; set; } // Prefeito escolhido
        public virtual Candidato candidatoPrefeito { get; set; }

        public int? idCandidatoVereador { get; set; } // Vereador escolhido
        public virtual Candidato candidatoVereador { get; set; }

        // Novo campo para o usuário que realiza a entrevista
        public int idUsuario { get; set; }
        public virtual Usuario usuario { get; set; }

        // Novo campo para o status da pesquisa
        public int idStatus { get; set; }
        public virtual Status status { get; set; }
        public virtual ICollection<Entrevistado> entrevistado { get; set; } = new List<Entrevistado>();
    }
}
