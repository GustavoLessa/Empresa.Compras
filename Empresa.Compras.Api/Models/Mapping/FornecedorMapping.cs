using Empresa.Compras.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Empresa.Compras.Api.Models.Mapping
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