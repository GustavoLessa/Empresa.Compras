using Empresa.Compras.Entities;
using FluentValidation;

namespace Empresa.Compras.Api.Models.Validation
{
    public class ForncedorValidator : AbstractValidator<Fornecedor>
    {
        public ForncedorValidator()
        {
            RuleFor(v => v.CnpjCpf)
                .NotEmpty().WithMessage("O CNPJ ou CPF do forncedor deve ser preenchido.");

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome do forncedor deve ser preenchido.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("O email forncedor deve ser preenchido.");
        }
    }
}