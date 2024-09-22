namespace MapeiaVoto.Application.Dto
{
    public class UsuarioDto
    {
        public int id { get; set; }
        public string nomeUsuario { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public int idStatus { get; set; }
        public int idPerfilUsuario { get; set; }
        // Nova propriedade para indicar se o usuário precisa trocar a senha
        public bool PrecisaTrocarSenha { get; set; }
    }
}
