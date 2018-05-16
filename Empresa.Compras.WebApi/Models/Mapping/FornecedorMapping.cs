using Empresa.Compras.WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Mapping
{
    public class FornecedorMapping : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMapping()
        {
            ToTable("Fornecedores");

            HasKey(v => v.IdFornecedor);

            Property(v => v.Nome).IsRequired().HasMaxLength(100);

            Property(v => v.CnpjCpf).IsRequired();
        }
    }
}