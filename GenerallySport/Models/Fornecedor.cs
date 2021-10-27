using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public int IdEndereco { get; set; }
    }
}
