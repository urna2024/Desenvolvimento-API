using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class RendaFamiliar : BaseEntity
    {
        public string nome { get; set; }
        public virtual ICollection<Entrevistado> entrevistado { get; set; } = new List<Entrevistado>();
    }
}
