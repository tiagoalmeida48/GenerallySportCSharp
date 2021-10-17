using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Fornecedor
    {
        public int Id { get; set; }
        public string Razaosocial { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public int IdEndereco { get; set; }
        public string Email { get; set; }
        public string Categoriaproduto { get; set; }
        public string Ativo
        {
            get; set;
        }
    }
}
