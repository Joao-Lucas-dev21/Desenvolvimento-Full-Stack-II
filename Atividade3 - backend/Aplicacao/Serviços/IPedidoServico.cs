using Atividade.Aplicacao.DTO;
using Atividade.Dominio.Entidade;

namespace Atividade.Aplicacao.Serviços
{
    public interface IPedidoServico
    {
        Task<List<Pedido>> GetAllAsync();
        Task <Pedido> GetByIdAsync(int id);
        Task<Pedido> CreateAsync(PedidoDTO pedidoDTO);
        Task <Pedido> UpdateAsync(int id, PedidoDTO pedidoDTO);
        Task <Pedido> DeleteAsync(int id);
        Task<Pedido> ProcessarPedido(Pedido pedido);
    }
}
