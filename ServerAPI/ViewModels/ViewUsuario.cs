using System.ComponentModel.DataAnnotations;

namespace ServerAPI.ViewModels
{
    public class ViewUsuario
    {
        [Required(ErrorMessage = "Nome Invalido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email Invalido")]
        [EmailAddress(ErrorMessage = "Email Invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Login Invalido")]
        [MinLength(1, ErrorMessage = "Login Curto")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha Invalido")]
        [MinLength(1, ErrorMessage = "Senha Curta")]
        public string Senha { get; set; }
    }
}
