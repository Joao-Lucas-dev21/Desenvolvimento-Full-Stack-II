using Atividade.Dominio.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Atividade.Infra
{
    public class AppDbContext : IdentityDbContext
    {
    
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<LogEstoque> LogsEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Descricao).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Pedido>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.NomeCliente).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<ItemPedido>(e =>
            {
                e.HasKey(e => new {e.PedidoId, e.ProdutoId });

                e.HasOne(x => x.Pedido)
                .WithMany(m => m.ItemPedidos)
                .HasForeignKey(x => x.PedidoId);
            });
        }

    }
}
