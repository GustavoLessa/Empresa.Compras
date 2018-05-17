using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Empresa.Compras.Entities
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposta> Propostas { get; set; }
    }
}