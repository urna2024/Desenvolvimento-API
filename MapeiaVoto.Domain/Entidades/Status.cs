﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Domain.Entidades
{
    public class Status : BaseEntity
    {
        public string nome { get; set; }
        public virtual ICollection<Candidato> candidato { get; set; } = new List<Candidato>();
        public virtual ICollection<Usuario> usuario { get; set; } = new List<Usuario>();
        public virtual ICollection<PesquisaEleitoralMunicipal> pesquisaeleitoralmunicipal { get; set; } = new List<PesquisaEleitoralMunicipal>();
    }
}
