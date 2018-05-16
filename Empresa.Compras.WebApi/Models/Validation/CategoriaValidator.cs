using Empresa.Compras.WebApi.Models.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Validation
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome da categoria deve ser preenchido")
                .Length(3, 100).WithMessage("O nome da Categoria deve ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}