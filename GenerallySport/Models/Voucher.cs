using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public string CaminhoFoto { get; set; }
        public string Ativo { get; set; }
        public string Fotoemstring { get; set; }
    }
}
