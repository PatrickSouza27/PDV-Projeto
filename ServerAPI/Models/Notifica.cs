using ServerAPI.Controllers.DbControllers;

namespace ServerAPI.Models
{
    public class Notifica
    {
        public int IdProduto { get; set; }
        public int IdUsuario { get; set; }
        public Notifica() { }
        public Notifica(int idProduto, int idUsuario)
        {
            IdProduto = idProduto;
            IdUsuario = idUsuario;
        }
        public bool Adicionar() => new ProdutosController().Notificar(this);
    }
}
