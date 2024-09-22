﻿using MapeiaVoto.Domain.Entidades;
using System.Text.Json.Serialization;

namespace MapeiaVoto.Application.Models
{
    public class UsuarioModel
    {
        public long id { get; set; }
        public string nomeUsuario { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public long idStatus { get; set; }
        public virtual Status status { get; set; }
        public long idPerfilUsuario { get; set; }
        public virtual PerfilUsuario perfilusuario { get; set; }

        // Nova propriedade para indicar se o usuário precisa trocar a senha/não precisa de = true, pois model somente transporta dados
        public bool PrecisaTrocarSenha { get; set; }
        [JsonIgnore]
        public virtual ICollection<PesquisaEleitoralMunicipal> pesquisaeleitoralmunicipal { get; set; } = new List<PesquisaEleitoralMunicipal>();
    }
}
