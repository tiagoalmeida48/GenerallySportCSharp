using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Carrinho
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código do cliente é obrigatório")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "O código do produto é obrigatório")]
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        public int? Qtde { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        public decimal Preco { get; set; }
    }
}
