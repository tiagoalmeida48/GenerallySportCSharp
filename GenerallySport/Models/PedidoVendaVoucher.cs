using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class PedidoVendaVoucher
    {
        public int Id { get; set; }
        public int IdVoucher { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataPedidovenda { get; set; }
        public string SituacaoPedidovenda { get; set; }
        public decimal ValorFinal { get; set; }
        public string Validado { get; set; }
        public string Condicaopagamento { get; set; }
    }
}
