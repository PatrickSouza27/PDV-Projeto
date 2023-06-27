using System.ComponentModel.DataAnnotations;

namespace ServerAPI.ViewModels
{
    public class ViewLogin
    {
        [Required(ErrorMessage = "Login Invalido")]
        [MaxLength(60, ErrorMessage = "Login Invalido, Contém Muito Caracter")]
        [MinLength(1, ErrorMessage = "Login Invalido, Contém pouco Caracter")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Valor Senha Invalida")]
        public string Senha { get; set; }
    }
}
