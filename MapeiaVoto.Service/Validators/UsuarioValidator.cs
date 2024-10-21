using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Infrastructure.Data.Context;

namespace MapeiaVoto.Service.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        private readonly SqlServerContext _context;

        public UsuarioValidator(SqlServerContext context)
        {
            _context = context;

            RuleFor(u => u.nomeUsuario)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .MaximumLength(255).WithMessage("O nome de usuário deve ter no máximo 255 caracteres.");

            RuleFor(u => u.email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .MaximumLength(255).WithMessage("O email deve ter no máximo 255 caracteres.")
                .EmailAddress().WithMessage("O email deve ser válido.")
                .Must(EmailUnique).WithMessage("O email já está em uso.");

            RuleFor(u => u.senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(255).WithMessage("A senha deve ter no máximo 255 caracteres.")
                .Must(ContainUppercaseLetter).WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Must(ContainNumber).WithMessage("A senha deve conter pelo menos um número.")
                .Must(ContainSpecialCharacter).WithMessage("A senha deve conter pelo menos um caractere especial.");

            RuleFor(u => u.idStatus)
                .Must(ValidarStatus).WithMessage("Status inválido.");

            RuleFor(u => u.idPerfilUsuario)
                .Must(ValidarPerfilUsuario).WithMessage("Perfil de usuário inválido.");
        }

        private bool ValidarStatus(int idStatus)
        {
            return _context.status.Any(st => st.id == idStatus);
        }

        private bool ValidarPerfilUsuario(int idPerfilUsuario)
        {
            return _context.perfilusuario.Any(pu => pu.id == idPerfilUsuario);
        }

        private bool EmailUnique(string email)
        {
            return !_context.usuario.Any(u => u.email == email);
        }

        private bool ContainUppercaseLetter(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]");
        }

        private bool ContainNumber(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"\d");
        }

        private bool ContainSpecialCharacter(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]");
        }
    }
}
