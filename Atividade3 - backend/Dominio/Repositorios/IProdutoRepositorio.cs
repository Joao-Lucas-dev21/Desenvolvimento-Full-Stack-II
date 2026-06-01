using Atividade.Dominio.Entidade;

namespace Atividade.Dominio.Repositorios
{
    public interface IProdutoRepositorio
    {
        Task <List<Produto>> GetAllAsync();

        Task<Produto?> GetByIdAsync(int id);

        Task AddAsync(Produto produto);

        Task UpdateAsync(Produto produto);

        Task DeleteAsync(Produto produto);
        

    }
}
