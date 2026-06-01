namespace Atividade.Dominio.Entidade
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }

        public ICollection<ItemPedido> ItemPedidos { get; set; } = new List<ItemPedido>();

        public Pedido(string NomeCliente)
        {
            NomeCliente = NomeCliente;

        }
    }
}
