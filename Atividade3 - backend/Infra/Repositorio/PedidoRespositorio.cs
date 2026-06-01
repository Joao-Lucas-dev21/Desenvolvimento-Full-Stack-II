using Atividade.Dominio.Entidade;
using Atividade.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Atividade.Infra.Repositorio
{
    public class PedidoRespositorio : IPedidoRepositorio
    {
        private readonly AppDbContext _context;

        public PedidoRespositorio(AppDbContext context) => _context = context;


        public async Task<List<Pedido>> GetAllAsync()
            => await _context.Pedidos
            .Include(m => m.ItemPedidos)
            .ThenInclude(mg => mg.Produto).ToListAsync();

        public async Task<Pedido?> GetByIdAsync(int id)
            => await _context.Pedidos
            .Include(m => m.ItemPedidos)
            .FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }

    }
}
