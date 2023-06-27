using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ProjetoSenac.Services;
using ServerAPI.Controllers.DbControllers;
using ServerAPI.Extension;
using ServerAPI.Models;
using ServerAPI.Services;
using ServerAPI.ViewModels;
using System.Data.Common;
using System.Security.Claims;

namespace ServerAPI.Controllers.EndPointControllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioControllers : ControllerBase
    {
        private readonly TokenServices _tokenService;
        public UsuarioControllers(TokenServices tokenService)
        {
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost("v1/conta")]
        public IActionResult Login([FromBody] ViewLogin login)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ViewModelResult<Usuario>(ModelState.GetError()));

            try
            {
                var usuarioLogin = new CriptografiaHash().VerificarUsuarioExiste(login);
                if (usuarioLogin is null)
                    return NotFound(new ViewModelResult<Usuario>("Usuario Não Encontrado"));

                var token = _tokenService.GerarToken(usuarioLogin);
                return Ok(new ViewModelResult<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ViewModelResult<string>("05XX1 - Falha Interna"));
            }
        }

        [AllowAnonymous]
        [HttpPost("v1/cadastrar")]
        public IActionResult Cadastrar([FromBody] ViewUsuario user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ViewModelResult<Usuario>(ModelState.GetError()));

            try
            {
                var usuario = new Usuario(user);
                if (!usuario.Adicionar())
                    return BadRequest(new ViewModelResult<Usuario>("Erro ao Cadastrar Usuario"));

                return Ok(new ViewModelResult<string>($"{user.Nome} Cadastrado com Sucesso", null));
            }
            catch(DbException erro)
            {
                return StatusCode(400, new ViewModelResult<string>("05XX2 - Este Login já Existe"));
            }
            catch
            {
                return StatusCode(500, new ViewModelResult<string>("05XX1 - Falha Interna"));
            }
        }
        [Authorize]
        [HttpPut("v1/editar")]
        public IActionResult Editar([FromBody] ViewUsuarioEditar user)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(new ViewModelResult<Usuario>(ModelState.GetError()));

            try
            {
                var id = User.FindFirstValue("Id");
                var users = new UsuarioController().Usuario(int.Parse(id));
                if(!users.Editar(user))
                    return BadRequest(new ViewModelResult<Usuario>("Erro ao editar Usuario"));

                return Ok(new ViewModelResult<string>($"{user.Nome} Editado com Sucesso", null));
            }
            catch (DbException erro)
            {
                return StatusCode(400, new ViewModelResult<string>("05XX2 - Chave Unica Violada"));
            }
            catch
            {
                return StatusCode(500, new ViewModelResult<string>("05XX1 - Falha Interna"));
            }
        }
        [Authorize]
        [HttpDelete("v1/deletar")]
        public IActionResult Excluir()
        {
            if (!ModelState.IsValid)
                return BadRequest(new ViewModelResult<Usuario>(ModelState.GetError()));

            try
            {
                var id = User.FindFirstValue("Id");
                var user = new UsuarioController().Usuario(int.Parse(id));
                if(!user.Excluir())
                    return BadRequest(new ViewModelResult<Usuario>("Erro ao Excluir Usuario"));

                return Ok(new ViewModelResult<string>($"Excluido com Sucesso", null));
            }
            catch (DbException erro)
            {
                return StatusCode(400, new ViewModelResult<string>("05XX2 - Erro ao Excluir Usuario Logado, existe alguma restrição em sua conta"));
            }
            catch
            {
                return StatusCode(500, new ViewModelResult<string>("05XX1 - Falha Interna"));
            }
        }
        [Authorize(Roles = "Gerente")]
        [HttpDelete("v1/admin/deletar/{id:int}")]
        public IActionResult DeletarId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ViewModelResult<Usuario>(ModelState.GetError()));
            var user = new UsuarioController().Usuario(id);
            if(user is null)
                 return NotFound(new ViewModelResult<Usuario>("Usuario Não Encontrado"));
            try
            {
                if (!user.Excluir())
                    return BadRequest(new ViewModelResult<Usuario>("Erro ao Excluir Usuario"));
                return Ok(new ViewModelResult<string>($"Excluido com Sucesso", null));
            }
            catch (DbException erro)
            {
                return StatusCode(400, new ViewModelResult<string>("05XX2 - Existe Produtos Vinculado a esse Usuario"));
            }
            catch
            {
                return StatusCode(500, new ViewModelResult<string>("05XX1 - Falha Interna"));
            }
        }
        [Authorize(Roles = "Gerente")]
        [HttpGet("v1/admin/todos")]
        public IActionResult TodosUsuarios()
            => Ok(new UsuarioController().TodosUsuario());
    }
}
