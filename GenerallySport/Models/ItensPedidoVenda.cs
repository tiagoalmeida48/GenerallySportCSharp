using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class ItensPedidoVenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do pedido é obrigatório")]
        public int IdPedidovenda { get; set; }

        public PedidoVenda PedidoVenda { get; set; }

        [Required(ErrorMessage = "O número do produto é obrigatório")]
        public int IdProduto { get; set; }

        public Produto Produto { get; set; }

        public int? Qtde { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        public decimal Preco { get; set; }
    }
}
