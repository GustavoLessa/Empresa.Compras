using System;
using System.ComponentModel.DataAnnotations;

namespace Empresa.Compras.Entities.Metadata
{
    public class CategoriaMetadata
    {
        [Required]
        public string Nome { get; set; }
    }

    public class FornecedorMetadata
    {
        [Required]
        public string CnpjCpf { get; set; }

        [Required]
        public string Nome { get; set; }       

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class PropostaMetadata
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DataProposta { get; set; }

        [Required]
        public decimal ValorProposta { get; set; }

        [Required]
        public int IdFornecedor { get; set; }

        [Required]
        public int IdCategoria { get; set; }
    }

    public class UsuarioMetadata
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Perfil { get; set; }
    }
}
