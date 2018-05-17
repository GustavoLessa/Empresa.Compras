using Empresa.Compras.Entities.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Empresa.Compras.Entities
{
    [MetadataType(typeof(CategoriaMetadata))]
    public partial class Categoria
    {
    }

    [MetadataType(typeof(FornecedorMetadata))]
    public partial class Fornecedor
    {
    }

    [MetadataType(typeof(PropostaMetadata))]
    public partial class Proposta
    {
    }

    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {
    }
}
