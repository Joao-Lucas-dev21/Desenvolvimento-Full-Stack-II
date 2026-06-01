using Atividade.Dominio.Entidade;

namespace Atividade.Dominio.Repositorios
{
    public interface IPedidoRepositorio
    {
        Task<List<Pedido>> GetAllAsync();

        Task<Pedido?> GetByIdAsync(int id);

        Task AddAsync(Pedido pedido);

        Task UpdateAsync(Pedido pedido);

        Task DeleteAsync(Pedido pedido);


    }
}
