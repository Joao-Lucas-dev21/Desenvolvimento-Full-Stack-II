using System.ComponentModel.DataAnnotations;

namespace Atividade.Aplicacao.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public int Estoque { get; set; }
    }
}