using Atividade.Aplicacao.DTO;
using Atividade.Dominio.Entidade;
using Atividade.Dominio.Repositorios;

namespace Atividade.Aplicacao.Serviços
{
    public class PedidoServico : IPedidoServico
    {
        private readonly IPedidoRepositorio _repositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        public PedidoServico(IPedidoRepositorio repositorio, IProdutoRepositorio produtoRepositorio)
        {
            _repositorio = repositorio;
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<List<Pedido>> GetAllAsync()
            => await _repositorio.GetAllAsync();

        public async Task<Pedido> GetByIdAsync(int id)
        {
            var pedido = await _repositorio.GetByIdAsync(id);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com ID {id} não encontrado.");
            return pedido;
        }

        public async Task<Pedido> CreateAsync(PedidoDTO pedidoDTO)
        {
            if (string.IsNullOrWhiteSpace(pedidoDTO.NomeCliente))
                throw new Exception("O nome do cliente é obrigatório.");

            var pedido = new Pedido(pedidoDTO.NomeCliente);

  
            var itensValidados = new List<ItemPedido>();

            foreach (var id in pedidoDTO.ProdutoIds)
            {

                var produto = await _produtoRepositorio.GetByIdAsync(id);
                if (produto == null)
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

                var item = new ItemPedido(produto, 1);

                itensValidados.Add(item);
            }

            pedido.ItemPedidos = itensValidados;
            return await ProcessarPedido(pedido);
        }

        public async Task<Pedido> UpdateAsync(int id, PedidoDTO pedidoDTO)
        {
            var pedido = await _repositorio.GetByIdAsync(id);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com ID {id} não encontrado.");

            typeof(Pedido).GetProperty("NomeCliente")?.SetValue(pedido, pedidoDTO.NomeCliente);
            await _repositorio.UpdateAsync(pedido);
            return pedido;
        }

        public async Task<Pedido> DeleteAsync(int id)
        {
            var pedido = await _repositorio.GetByIdAsync(id);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com ID {id} não encontrado.");
            await _repositorio.DeleteAsync(pedido);
            return pedido;
        }


        public async Task<Pedido> ProcessarPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido), "O pedido não pode ser nulo.");

            if (pedido.ItemPedidos == null || !pedido.ItemPedidos.Any())
                throw new Exception("O pedido deve conter pelo menos um item.");

            foreach (var item in pedido.ItemPedidos)
            {

                var produto = await _produtoRepositorio.GetByIdAsync(item.ProdutoId);
                if (produto == null)
                    throw new KeyNotFoundException($"Produto com ID {item.ProdutoId} não encontrado.");


                produto.DebitarEstoque(item.Quantidade);

                await _produtoRepositorio.UpdateAsync(produto);
            }

            await _repositorio.AddAsync(pedido);

            return pedido;
        }
    }
}