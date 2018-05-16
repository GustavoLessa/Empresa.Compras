using Empresa.Compras.WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Mapping
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