using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Data.SqlClient;
using ServerAPI;
using ServerAPI.Models;
using ServerAPI.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace ProjetoSenac.Services
{
    public class CriptografiaHash
    {
        public string CriptografarSenha(string senha)
             => new PasswordHasher<object>().HashPassword(null, senha);

        public Usuario? VerificarUsuarioExiste(ViewLogin login)
        {

            using var conn = new SqlConnection(Configuration.Conexao);

            var userLogin = conn.Query<Usuario>("SELECT * FROM [Usuario] WHERE [Login] = @Login", new { login.Login }).FirstOrDefault();
            if(userLogin == null)
                return null;


            return new PasswordHasher<object>().VerifyHashedPassword(null, userLogin.Senha, login.Senha) == PasswordVerificationResult.Success ? userLogin : null;

        }
    }
}