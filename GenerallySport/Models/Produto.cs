using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Produto
    {
        public int Id { get; set; }
        public int IdFornecedor { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? QtdeEstoque { get; set; }
        public int? QtdeEstoqueatual { get; set; }

        public string CaminhoFoto { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Inativo { get; set; }
        public string Fotoemstring { get; set; }
    }
}
