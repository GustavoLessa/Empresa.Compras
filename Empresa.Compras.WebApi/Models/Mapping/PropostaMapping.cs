using Empresa.Compras.WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Mapping
{
    public class PropostaMapping : EntityTypeConfiguration<Proposta>
    {
        public PropostaMapping()
        {
            ToTable("Propostas");

            HasKey(v => v.IdProposta);

            Property(v => v.Nome).IsRequired().HasMaxLength(100);
            Property(v => v.DataProposta).IsRequired();

            HasRequired<Fornecedor>(r => r.Fornecedor).WithMany(v => v.Propostas)
                                          .WillCascadeOnDelete();

            
        }
    }
}