using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Produto
    {
        public int Id { get; set; }

        [Column("ID_FORNECEDOR")]
        public int IdFornecedor { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }

        [Column("DATA_VALIDADE")]
        public DateTime? DataValidade { get; set; }

        [Column("QTDE_ESTOQUE")]
        public int? QtdeEstoque { get; set; }

        [Column("QTDE_ESTOQUEATUAL")]
        public int? QtdeEstoqueatual { get; set; }

        [Column("CAMINHO_FOTO")]
        public string CaminhoFoto { get; set; }

        [Column("PRECO_UNITARIO")]
        public decimal PrecoUnitario { get; set; }

        [Column("PRECO_VENDA")]
        public decimal PrecoVenda { get; set; }
        public string Inativo { get; set; }
    }
}
