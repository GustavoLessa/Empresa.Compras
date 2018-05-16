using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Entities
{
    public class Categoria
    {
        public int IdCategoria { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposta> Propostas { get; set; }
    }
}