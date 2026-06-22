using Atividade.Api.Controle;
using Atividade.Aplicacao.DTO;
using Atividade.Dominio.Entidade;
using Atividade.Dominio.Repositorios;
using Atividade.Infra;

namespace Atividade.Aplicacao.Serviços
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _repositorio;
        private readonly AppDbContext _context;

        public ProdutoServico(IProdutoRepositorio repositorio, AppDbContext context)
        {
            _repositorio = repositorio;
            _context = context;
        }

        public async Task<List<Produto>> GetAllAsync()
            => await _repositorio.GetAllAsync();

        public async Task<Produto> CreateAsync(ProdutoDTO produtoDTO)
        {
            if (string.IsNullOrWhiteSpace(produtoDTO.Descricao))
                throw new ArgumentException("O nome do produto é obrigatório.");

            var produto = new Produto(produtoDTO.Descricao);
            await _repositorio.AddAsync(produto);
            return produto;
        }

        public async Task UpdateAsync(int id, ProdutoDTO produtoDTO)
        {
            var produto = await _repositorio.GetByIdAsync(id);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado");
            var propDescricao = typeof(Produto).GetProperty("Descricao") ?? typeof(Produto).GetProperty("descricao");
            var propEstoque = typeof(Produto).GetProperty("Estoque") ?? typeof(Produto).GetProperty("estoque");

            if (propDescricao != null) propDescricao.SetValue(produto, produtoDTO.Descricao);
            if (propEstoque != null) propEstoque.SetValue(produto, produtoDTO.Estoque);

            await _repositorio.UpdateAsync(produto);
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _repositorio.GetByIdAsync(id);

            if (produto == null)
                throw new ArgumentException("Produto não encontrado");
            await _repositorio.DeleteAsync(produto);
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var produto = await _repositorio.GetByIdAsync(id);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            return produto;
        }

        public async Task FinalizarPedidoAsync(PedidoComando comando)
        {
            if (comando == null || !comando.Itens.Any())
                throw new ArgumentException("O pedido precisa conter pelo menos um item.");
            await Task.CompletedTask;
        }

        public async Task AtualizarEstoqueComLogAsync(int produtoId, int novaQuantidade, int usuarioId)
        {
            var produto = await _repositorio.GetByIdAsync(produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            var propEstoque = typeof(Produto).GetProperty("Estoque") ?? typeof(Produto).GetProperty("estoque");
            if (propEstoque == null)
                throw new Exception("Propriedade de estoque não encontrada na entidade Produto.");

            propEstoque.SetValue(produto, novaQuantidade);
            await _repositorio.UpdateAsync(produto);

            var log = new LogEstoque(produtoId, usuarioId, novaQuantidade);

            await _context.LogsEstoque.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}