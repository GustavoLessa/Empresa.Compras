using Newtonsoft.Json;
using System.Collections.Generic;

namespace Empresa.Compras.Entities
{
    public partial class Categoria
    {
        public int IdCategoria { get; set; }
       
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proposta> Propostas { get; set; }
    }
}