using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Usuario1 { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string CaminhoFoto { get; set; }
        public string Fotoemstring { get; set; }
        public string Hash { get; set; }
        public string Ativo { get; set; }
    }
}
