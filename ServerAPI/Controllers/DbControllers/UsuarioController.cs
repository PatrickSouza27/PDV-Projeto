using Dapper;
using Microsoft.Data.SqlClient;
using ProjetoSenac.Services;
using ServerAPI.Models;
using ServerAPI.ViewModels;

namespace ServerAPI.Controllers.DbControllers
{
    public class UsuarioController
    {
        public List<Usuario> TodosUsuario()
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Query<Usuario>("SELECT * FROM Usuario").ToList();
        }
        public Usuario? Usuario(int id)
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Query<Usuario>("SELECT * FROM [Usuario] WHERE [Id] = @id", new {id}).FirstOrDefault();
        }
        public bool Adicionar(Usuario user)
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Execute("INSERT INTO [Usuario](Nome, Email, Acesso, Login, Senha) VALUES (@Nome, @Email, @Acesso, @Login, @Senha)", new { user.Nome, user.Email, user.Acesso, user.Login, Senha = new CriptografiaHash().CriptografarSenha(user.Senha)}) == 1;
        }
        public bool Editar(Usuario user)
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Execute("UPDATE [Usuario] SET [Nome] = @Nome, [Email] = @Email, [Senha] = @Senha WHERE [Id] = @Id", new { user.Nome, user.Email, Senha = new CriptografiaHash().CriptografarSenha(user.Senha), user.ID }) == 1;
        }
        public bool Excluir(int Id)
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Execute("DELETE [Usuario] WHERE [Id] = @Id", new { Id }) == 1;
        }
    }
}
