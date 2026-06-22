namespace Atividade.Dominio.Entidade
{
    public class LogEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int UsuarioId { get; set; }
        public int QuantidadeAlterada { get; set; }
        public DateTime DataAlteracao { get; set; }

        public LogEstoque() { }

        public LogEstoque(int produtoId, int usuarioId, int quantidadeAlterada)
        {
            ProdutoId = produtoId;
            UsuarioId = usuarioId;
            QuantidadeAlterada = quantidadeAlterada;
            DataAlteracao = DateTime.Now;
        }
    }
}