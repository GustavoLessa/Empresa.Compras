using Empresa.Compras.Entities;
using FluentValidation;

namespace Empresa.Compras.Api.Models.Validation
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome do usuário deve ser preenchido.")
                .Length(3, 100).WithMessage("O nome do usuário deve ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(v => v.Cpf)
                .NotEmpty().WithMessage("O CPF do usuário deve ser preenchida.");

            RuleFor(v => v.Perfil)
                 .NotEmpty().WithMessage("O peril deve ser informado.");
           
        }
    }
}