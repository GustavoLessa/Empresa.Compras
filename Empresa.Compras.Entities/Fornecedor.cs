using Newtonsoft.Json;
using System.Collections.Generic;

namespace Empresa.Compras.Entities
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