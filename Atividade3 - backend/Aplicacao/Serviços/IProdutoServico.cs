using Atividade.Aplicacao.DTO;
using Atividade.Dominio.Entidade;
using Atividade.Api.Controle;

namespace Atividade.Aplicacao.Serviços
{
    public interface IProdutoServico
    {
        Task<List<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> CreateAsync(ProdutoDTO produtoDTO);
        Task UpdateAsync(int id, ProdutoDTO produtoDTO);
        Task DeleteAsync (int id);
        Task FinalizarPedidoAsync(PedidoComando comando);
        Task AtualizarEstoqueComLogAsync(int produtoId, int novaQuantidade, int usuarioId);
    }
}
