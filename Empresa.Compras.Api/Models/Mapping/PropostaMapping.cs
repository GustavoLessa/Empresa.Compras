using Empresa.Compras.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Empresa.Compras.Api.Models.Mapping
{
    public class PropostaMapping : EntityTypeConfiguration<Proposta>
    {
        public PropostaMapping()
        {
            ToTable("Propostas");

            HasKey(v => v.IdProposta);

            Property(v => v.Nome).IsRequired().HasMaxLength(100);
            Property(v => v.DataProposta).IsRequired();
            Property(v => v.ValorProposta).IsRequired();

            HasRequired<Fornecedor>(r => r.Fornecedor).WithMany(v => v.Propostas)
                                          .WillCascadeOnDelete();

            HasRequired<Categoria>(r => r.Categoria).WithMany(v => v.Propostas)
                                          .WillCascadeOnDelete();
        }
    }
}