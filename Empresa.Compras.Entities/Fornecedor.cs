using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Empresa.Compras.Entities
{
    public class Fornecedor
    {
        [Required]
        public int IdFornecedor { get; set; }

        [Required]
        public string CnpjCpf { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposta> Propostas { get; set; }
    }
}