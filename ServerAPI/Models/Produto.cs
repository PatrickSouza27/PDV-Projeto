namespace ServerAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public double Valor { get; set; }
        public double Quantidade { get; set; }
        public string Medida { get; set; }
    }
}
