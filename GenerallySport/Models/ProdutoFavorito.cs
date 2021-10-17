using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class ProdutoFavorito
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Produto { get; set; }
        public string Descricao { get; set; }
        public int? QtdeCarrinho { get; set; }
        public decimal Precovenda { get; set; }
        public string Caminhofoto { get; set; }
    }
}
