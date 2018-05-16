using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Entities
{
    public class Proposta
    {
        public int IdProposta { get; set; }

        public string Nome { get; set; }              

        public DateTime DataProposta { get; set; }

        public string DescricaoGeral { get; set; }

        public int IdFornecedor { get; set; }

        public int IdCategoria { get; set; }

        [JsonIgnore]
        public virtual Fornecedor Fornecedor { get; set; }

        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }

      


    }
}