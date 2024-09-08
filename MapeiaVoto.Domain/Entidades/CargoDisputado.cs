using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class CargoDisputado : BaseEntity
    {
        public string nome { get; set; }
        public virtual ICollection<Candidato> candidato { get; set; } = new List<Candidato>();
    }
}
