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
            // Validação para o campo 'nome'
            RuleFor(p => p.nome)
                .NotEmpty().WithMessage("O nome do Nível de Escolaridade é obrigatório.")  // Verifica se o nome não está vazio
                .Length(1, 100).WithMessage("O nome do Nível de Escolaridade pode ter entre 1 e 100 caracteres.");

        }
    }
}
