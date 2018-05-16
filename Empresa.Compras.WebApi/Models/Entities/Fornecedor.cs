using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.Compras.WebApi.Models.Entities
{
    public class Fornecedor
    {
        public int IdFornecedor { get; set; }

        public string CnpjCpf { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposta> Propostas { get; set; }
    }
}