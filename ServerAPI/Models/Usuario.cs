using ProjetoSenac.Models.Enums;
using ServerAPI.Controllers.DbControllers;
using ServerAPI.ViewModels;

namespace ServerAPI.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public AcessoEnum Acesso { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario() { }
        public Usuario(ViewUsuario user)
        {
            Nome = user.Nome;
            Email = user.Email;
            Acesso = AcessoEnum.Cliente;
            Login = user.Login;
            Senha = user.Senha;
        }

        public bool Adicionar() => new UsuarioController().Adicionar(this);
        public bool Editar(ViewUsuarioEditar editar)
        {
            Nome = editar.Nome;
            Email = editar.Email;
            Senha = editar.Senha;
            return new UsuarioController().Editar(this);
        }
        public bool Excluir() => new UsuarioController().Excluir(this.ID);

    }
}
