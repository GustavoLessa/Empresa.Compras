using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.Compras.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Perfil { get; set; }
    }
}