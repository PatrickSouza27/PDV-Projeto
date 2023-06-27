using Dapper;
using Microsoft.Data.SqlClient;
using ServerAPI.Models;

namespace ServerAPI.Controllers.DbControllers
{
    public class ProdutosController
    {
        public List<Produto> TodosProdutos()
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Query<Produto>("SELECT [Id], [Nome], [Categoria], [Valor], [Quantidade], [Medida] FROM [ViewDataGridProduto]").ToList();
        }
        public List<Produto> BuscarPorNome(string nome)
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Query<Produto>("SELECT [Id], [Nome], [Categoria], [Valor], [Quantidade], [Medida] FROM [ViewDataGridProduto] WHERE [Nome] LIKE '%'+@nome+'%'", new { nome }).ToList();
        }
        public bool Notificar(Notifica notifica)
        {
            using var conn = new SqlConnection(Configuration.Conexao);
            return conn.Execute("INSERT INTO [Notifica](IdUsuario, IdProduto) VALUES (@IdUsuario, @IdProduto)", new { notifica.IdUsuario, notifica.IdProduto }) == 1;
        }
        public List<Produto> BuscarProdutos(List<int> idProdutos)
        {
            using (var conn = new SqlConnection(Configuration.Conexao))
            {
                return conn.Query<Produto>("SELECT [Id], [Nome], [Categoria], [Valor], [Quantidade], [Medida] FROM [ViewDataGridProduto] WHERE [Quantidade] > 0 AND [Id] IN @IdProdutos", new { idProdutos }).ToList();
            }
        }

        public List<Notifica> Notificacoes(int idUsuario)
        {
            using (var conn = new SqlConnection(Configuration.Conexao))
            {
                return conn.Query<Notifica>("SELECT * FROM [Notifica] WHERE [IdUsuario] = @IdUsuario", new { IdUsuario = idUsuario }).ToList();
            }
        }
    }
}
