using FluentValidation;
using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Service.Validators
{
    public class PartidoPoliticoValidator : AbstractValidator<PartidoPolitico>
    {
        public PartidoPoliticoValidator()
        {
            // Nome não pode ser vazio
            RuleFor(p => p.nome)
                .NotEmpty().WithMessage("Informe o nome do Partido!")
                .NotNull().WithMessage("Informe o nome do Partido!")
                .MaximumLength(100).WithMessage("O nome do Partido pode ter no máximo 100 caracteres.");

            // Sigla não pode ser vazia
            RuleFor(p => p.sigla)
                .NotEmpty().WithMessage("Informe a sigla do Partido!")
                .NotNull().WithMessage("Informe a sigla do Partido!")
                .MaximumLength(10).WithMessage("A sigla do Partido pode ter no máximo 10 caracteres.");

            
        }
    }
}
