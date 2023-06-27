using System.ComponentModel.DataAnnotations;

namespace ServerAPI.ViewModels
{
    public class ViewUsuarioEditar
    {
        [Required(ErrorMessage = "Nome Invalido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email Invalido")]
        [EmailAddress(ErrorMessage = "Email Invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Invalido")]
        [MinLength(1, ErrorMessage = "Senha Curta")]
        public string Senha { get; set; }
    }
}
