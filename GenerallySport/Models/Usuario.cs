using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenerallySport.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }

        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "O e-mail informado é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
        public string CaminhoFoto { get; set; }
    }
}
