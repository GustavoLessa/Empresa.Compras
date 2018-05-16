using Empresa.Compras.WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Mapping
{
    public class CategoriaMapping : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMapping()
        {
            ToTable("Categorias");

            HasKey(v => v.IdCategoria);

            Property(v => v.Nome).IsRequired().HasMaxLength(100);
            
        }
    }
}