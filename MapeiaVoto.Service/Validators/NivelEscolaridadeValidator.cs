using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Service.Validators
{
    public class NivelEscolaridadeValidator : AbstractValidator<NivelEscolaridade>
    {
        public NivelEscolaridadeValidator()
        {
            // Nome não pode ser vazio
            RuleFor(p => p.nome)
                .NotEmpty().WithMessage("Informe o nome do Nível Escolar!")
                .NotNull().WithMessage("Informe o nome do Nível Escolar!")
                .MaximumLength(100).WithMessage("O nome do Nível Escolar pode ter no máximo 100 caracteres.");

           


        }
    }
}
