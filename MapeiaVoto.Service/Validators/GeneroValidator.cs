using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Service.Validators
{
    public class GeneroValidator : AbstractValidator<Genero>
    {
        public GeneroValidator()
        {
            // Nome não pode ser vazio
            RuleFor(e => e.nome)
                .NotEmpty().WithMessage("Informe o nome do Gênero!")
                .NotNull().WithMessage("Informe o nome do Gênero!");

        }
    }
}
