using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Empresa.Compras.Entities
{
    public class Proposta
    {
        public int IdProposta { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataProposta { get; set; }

        public string DescricaoGeral { get; set; }

        [Required]
        public decimal ValorProposta { get; set; }
        
        public string Status { get; set; }

        public bool AprovadoPeloAnalista { get; set; }

        public bool AprovadoPeloDiretor { get; set; }

        [Required]
        public int IdFornecedor { get; set; }

        [Required]
        public int IdCategoria { get; set; }
        
        public virtual Fornecedor Fornecedor { get; set; }
        
        public virtual Categoria Categoria { get; set; }
    }
}