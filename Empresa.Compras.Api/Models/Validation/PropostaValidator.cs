using Empresa.Compras.Entities;
using FluentValidation;

namespace Empresa.Compras.Api.Models.Validation
{
    public class PropostaValidator : AbstractValidator<Proposta>
    {
        public PropostaValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome da proposta deve ser preenchido")
                .Length(3, 100).WithMessage("O nome da proposta deve ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(e => e.DataProposta)
               .NotEmpty().WithMessage("A data da proposta deve ser preenchida");

            RuleFor(e => e.ValorProposta)
              .NotEmpty().WithMessage("O Valor da proposta deve ser preenchida");
        }
    }
}