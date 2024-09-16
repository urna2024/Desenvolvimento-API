﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class PerfilUsuario : BaseEntity
    {
        public string nome { get; set; }
        public virtual ICollection<Usuario> usuario { get; set; } = new List<Usuario>();
    }
}
