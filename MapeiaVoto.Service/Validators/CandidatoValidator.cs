using FluentValidation;
using MapeiaVoto.Domain.Entidades;

namespace MapeiaVoto.Service.Validators
{
    public class CandidatoValidator : AbstractValidator<Candidato>
    {
        public CandidatoValidator()
        {
            
            RuleFor(c => c.nomeCompleto)
                .NotEmpty().WithMessage("O nome completo é obrigatório.")
                .MaximumLength(150).WithMessage("O nome completo pode ter no máximo 150 caracteres.");

          
            RuleFor(c => c.nomeUrna)
                .NotEmpty().WithMessage("O nome de urna é obrigatório.")
                .MaximumLength(150).WithMessage("O nome de urna pode ter no máximo 150 caracteres.");


            
            RuleFor(c => c.uf)
                .NotEmpty().WithMessage("A UF é obrigatória.")
                .Length(2).WithMessage("A UF deve conter 2 caracteres.");

           
            RuleFor(c => c.municipio)
                .NotEmpty().WithMessage("O município é obrigatório.")
                .MaximumLength(100).WithMessage("O município pode ter no máximo 100 caracteres.");

            
            RuleFor(c => c.foto)
                .MaximumLength(255).WithMessage("O caminho da foto pode ter no máximo 255 caracteres.");

           
            RuleFor(c => c.idStatus)
                .NotEmpty().WithMessage("O status é obrigatório.");

           
            RuleFor(c => c.idCargoDisputado)
                .NotEmpty().WithMessage("O cargo disputado é obrigatório.");

            
            RuleFor(c => c.idPartidoPolitico)
                .NotEmpty().WithMessage("O partido político é obrigatório.");
        }
    }
}
