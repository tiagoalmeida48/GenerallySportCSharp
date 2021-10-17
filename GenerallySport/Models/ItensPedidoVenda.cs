using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class ItensPedidoVenda
    {
        public int Id { get; set; }
        public int IdPedidovenda { get; set; }
        public int IdProduto { get; set; }
        public int? Qtde { get; set; }
        public decimal Preco { get; set; }
    }
}
