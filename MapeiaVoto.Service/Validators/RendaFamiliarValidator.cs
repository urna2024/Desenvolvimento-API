using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Service.Validators
{
    public class RendaFamiliarValidator : AbstractValidator<RendaFamiliar>
    {
        public RendaFamiliarValidator()
        {
            // Validação para o campo 'nome'
            RuleFor(r => r.nome)
                .NotEmpty().WithMessage("O nome da renda familiar é obrigatório.")
                .Length(1, 100).WithMessage("O nome da renda familiar deve ter entre 1 e 100 caracteres.");
        }
    }
}
