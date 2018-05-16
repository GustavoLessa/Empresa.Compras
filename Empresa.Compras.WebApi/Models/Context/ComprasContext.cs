using Empresa.Compras.WebApi.Models.Entities;
using Empresa.Compras.WebApi.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Context
{
    public class ComprasContext : DbContext 
    {
        public ComprasContext() : base("DbCompras")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Usuario>(new UsuarioMapping());
            modelBuilder.Configurations.Add<Categoria>(new CategoriaMapping());
            modelBuilder.Configurations.Add<Fornecedor>(new FornecedorMapping());
            modelBuilder.Configurations.Add<Proposta>(new PropostaMapping());
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Proposta> Propostas { get; set; }
    }
}