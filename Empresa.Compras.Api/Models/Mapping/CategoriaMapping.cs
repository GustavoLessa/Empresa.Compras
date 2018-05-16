using Empresa.Compras.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Empresa.Compras.Api.Models.Mapping
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