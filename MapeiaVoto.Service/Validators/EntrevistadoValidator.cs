using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Service.Validators
{
    public class EntrevistadoValidator : AbstractValidator<Entrevistado>
    {
        public EntrevistadoValidator()
        {
            // Nome não pode ser vazio
            RuleFor(e => e.nomeCompleto)
                .NotEmpty().WithMessage("Informe o nome do entrevistado!")
                .NotNull().WithMessage("Informe o nome do entrevistado!");

            // Data de nascimento não pode ser vazia e deve ser uma data válida
            RuleFor(e => e.dataNascimento)
                .NotEmpty().WithMessage("Informe a data de nascimento do entrevistado!")
                .NotNull().WithMessage("Informe a data de nascimento do entrevistado!")
                .Must(BeAValidDate).WithMessage("Data de nascimento inválida!");

            // UF não pode ser vazia e deve ter exatamente 2 caracteres
            RuleFor(e => e.uf)
                .NotEmpty().WithMessage("Informe a UF do entrevistado!")
                .NotNull().WithMessage("Informe a UF do entrevistado!")
                .Length(2).WithMessage("A UF deve ter 2 caracteres!");

            // Município não pode ser vazio
            RuleFor(e => e.municipio)
                .NotEmpty().WithMessage("Informe o município do entrevistado!")
                .NotNull().WithMessage("Informe o município do entrevistado!");

            // Celular é opcional, mas se fornecido deve ter um formato válido
            RuleFor(e => e.celular)
                .Matches(@"^\+?[0-9]{10,15}$").When(e => !string.IsNullOrEmpty(e.celular))
                .WithMessage("Informe um número de celular válido!");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
