using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Controllers.DbControllers;
using ServerAPI.Models;
using ServerAPI.ViewModels;
using System.Security.Claims;

namespace ServerAPI.Controllers.EndPointControllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        public IActionResult TodosUsuarios()
            => Ok(new ProdutosController().TodosProdutos());

        [HttpGet("{nome}")]
        public IActionResult ProdutoNome(string nome)
        {
            var list = new ProdutosController().BuscarPorNome(nome);
            if (list.Count is 0)
                return NotFound(new ViewModelResult<Produto>("Nenhum Produto Encontrado com esse Nome"));

            return Ok(new ProdutosController().BuscarPorNome(nome));
        }

        [Authorize]
        [HttpPost("notifica")]
        public IActionResult VinculaNotifica(NotificaProduto prod)
        {
            var list = new Produto();
            if (int.TryParse(prod.Produto, out var val))
                list = new ProdutosController().TodosProdutos().Where(x => x.Id == val).FirstOrDefault();
            else
            {
                list = new ProdutosController().TodosProdutos().Where(x => x.Nome == prod.Produto).FirstOrDefault();
            }
            if (list is null)
                return NotFound(new ViewModelResult<Produto>("Nenhum Produto Encontrado com esse Id"));
            if(list.Quantidade > 0)
                return BadRequest(new ViewModelResult<Produto>("Esse Produto ainda tem estoque"));

            var id = User.FindFirstValue("Id");


            if (new Notifica(list.Id, int.Parse(id)).Adicionar())
                return Ok(new ViewModelResult<string>("Produto Vinculado com sucesso, assim que chegar, Enviaremos no seu Email Cadastrado ou veja em suas notificações!", null));
            else
                return BadRequest(new ViewModelResult<Produto>("Erro ao Vincular"));

        }
        [Authorize]
        [HttpGet("notifica")]
        public IActionResult VerificaNotifica()
        {
            var idUsuario = User.FindFirstValue("Id");
            var listaNotificacoes = new ProdutosController().Notificacoes(int.Parse(idUsuario));

            if (listaNotificacoes == null)
                return NotFound(new ViewModelResult<Produto>("Nenhum Produto Vinculado para ser notificado"));

            var idProdutos = listaNotificacoes.Select(n => n.IdProduto).ToList();
            var listProdutos = new ProdutosController().BuscarProdutos(idProdutos);

            if (listProdutos.Count == 0)
                return StatusCode(400, new ViewModelResult<Produto>("Nenhuma notificação disponível no momento."));

            return Ok(new ViewModelResult<dynamic>(new
            {
                QuantidadeRegistro = listProdutos.Count,
                ListProdutos = listProdutos
            }));

        }

    }
}
