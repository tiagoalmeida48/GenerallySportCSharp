using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class PedidoVenda
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do cliente é obrigatório")]
        public int IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória")]
        public DateTime DataPedidovenda { get; set; }

        [Required(ErrorMessage = "A data da entrega é obrigatória")]
        public DateTime DataEntregavenda { get; set; }

        [Required(ErrorMessage = "A situação do pedido é obrigatória")]
        public string SituacaoPedidovenda { get; set; }

        [Required(ErrorMessage = "A condição de pagamento é obrigatória")]
        public string CondicaoPagamento { get; set; }

        [Required(ErrorMessage = "O valor final é obrigatório")]
        public decimal ValorFinal { get; set; }
    }
}
