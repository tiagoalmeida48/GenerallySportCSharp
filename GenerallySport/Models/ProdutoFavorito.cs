using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class ProdutoFavorito
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }

        public Produto Produto { get; set; }
    }
}
