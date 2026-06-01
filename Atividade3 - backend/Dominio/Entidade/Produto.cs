namespace Atividade.Dominio.Entidade
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } 
        public int Estoque { get; set; }

        public Produto(string descricao)
        {
            this.Descricao = descricao;
            this.Estoque = 10; 
        }

        public void DebitarEstoque(int quantidade)
        {
            if (Estoque < quantidade)
                throw new InvalidOperationException("Estoque insuficiente.");
            Estoque -= quantidade;
        }
    }
} 