using Empresa.Compras.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Empresa.Compras.Api.Models.Mapping
{
    public class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("Usuarios");

            HasKey(v => v.IdUsuario);

            Property(v => v.Nome).IsRequired().HasMaxLength(100);
            Property(v => v.Cpf).IsRequired().HasMaxLength(14);
            Property(v => v.Perfil).IsRequired();
        }
    }
}