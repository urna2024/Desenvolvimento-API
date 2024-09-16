using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class Usuario : BaseEntity
    {
        public string nomeUsuario { get; set; }
        public string senha { get; set; }
        public int idStatus { get; set; }
        public virtual Status status { get; set; }
        public int idPerfilUsuario { get; set; }
        public virtual PerfilUsuario perfilusuario { get; set; }
        public bool precisaTrocarSenha { get; set; } = true; // Padrão é true para novos usuários
    }
}
