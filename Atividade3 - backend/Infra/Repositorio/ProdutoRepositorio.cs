using Atividade.Dominio.Entidade;
using Atividade.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Atividade.Infra.Repositorio


{
    public class ProdutoRepositorio : IProdutoRepositorio

    {
        private readonly AppDbContext _context;

        public ProdutoRepositorio(AppDbContext context) => _context = context;

        public async Task<List<Produto>> GetAllAsync()
            => await _context.Produtos.ToListAsync();

        public async Task<Produto?> GetByIdAsync(int id)
            => await _context.Produtos.FindAsync(id);

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }



    }
}
