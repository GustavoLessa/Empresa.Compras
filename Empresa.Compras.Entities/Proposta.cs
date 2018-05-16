using Newtonsoft.Json;
using System;

namespace Empresa.Compras.Entities
{
    public class Proposta
    {
        public int IdProposta { get; set; }

        public string Nome { get; set; }              

        public DateTime DataProposta { get; set; }

        public string DescricaoGeral { get; set; }

        public decimal ValorProposta { get; set; }

        public string Status { get; set; }

        public bool AprovadoPeloAnalista { get; set; }

        public bool AprovadoPeloDiretor { get; set; }

        public int IdFornecedor { get; set; }

        public int IdCategoria { get; set; }

        [JsonIgnore]
        public virtual Fornecedor Fornecedor { get; set; }

        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }

      


    }
}