using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Empresa.Compras.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }

        public DateTime? DataNascimento { get; set; }

        [Required]
        public string Perfil { get; set; }
    }
}