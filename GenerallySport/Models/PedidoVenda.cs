using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class PedidoVenda
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataPedidovenda { get; set; }
        public DateTime DataEntregavenda { get; set; }
        public string SituacaoPedidovenda { get; set; }
        public string CondicaoPagamento { get; set; }
        public decimal ValorFinal { get; set; }
        public string Ativo { get; set; }
    }
}
