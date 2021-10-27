using System.ComponentModel.DataAnnotations;

namespace GenerallySport.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é abrigatória")]
        public string Senha { get; set; }
    }
}
