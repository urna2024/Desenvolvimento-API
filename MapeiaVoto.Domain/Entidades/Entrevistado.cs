using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class Entrevistado : BaseEntity
    {
        public string nomeCompleto { get; set; }
        public DateTime dataNascimento { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public string? celular { get; set; }
    }
}
