namespace Atividade.Dominio.Entidade
{
    public class ItemPedido
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }


        protected ItemPedido() { }


        public ItemPedido(Produto produto, int quantidade)
        {
            this.Produto = produto;
            this.ProdutoId = produto.Id;
            this.Quantidade = quantidade;
        }
    }
}