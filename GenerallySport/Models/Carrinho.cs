﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Carrinho
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
        public int? Qtde { get; set; }
        public decimal Preco { get; set; }
    }
}
