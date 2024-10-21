using FluentValidation;

namespace MapeiaVoto.Application.Dto
{
    public class UsuarioTrocarSenhaDto
    {
        public int id { get; set; }
        public string senha { get; set; }
        public string novaSenha { get; set; }
    }

    public class UsuarioTrocarSenhaDtoValidator : AbstractValidator<UsuarioTrocarSenhaDto>
    {
        public UsuarioTrocarSenhaDtoValidator()
        {
            // A nova senha deve ser diferente da senha atual
            RuleFor(x => x.novaSenha)
                .NotEmpty().WithMessage("A nova senha é obrigatória.")
                .MinimumLength(8).WithMessage("A nova senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(255).WithMessage("A nova senha deve ter no máximo 255 caracteres.")
                .Must(ContainUppercaseLetter).WithMessage("A nova senha deve conter pelo menos uma letra maiúscula.")
                .Must(ContainNumber).WithMessage("A nova senha deve conter pelo menos um número.")
                .Must(ContainSpecialCharacter).WithMessage("A nova senha deve conter pelo menos um caractere especial.")
                .NotEqual(x => x.senha).WithMessage("A nova senha não pode ser igual à senha atual.");

            // A senha atual é obrigatória
            RuleFor(x => x.senha)
                .NotEmpty().WithMessage("A senha atual é obrigatória.");
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
