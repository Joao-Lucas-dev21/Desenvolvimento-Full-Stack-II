using Atividade.Aplicacao.Serviços;
using Microsoft.AspNetCore.Mvc;

namespace Atividade.Api.Controle
{
    [Route("api/pedido")]
    [ApiController]
    public class FinalizarPedidoControle : ControllerBase
    {
        private readonly IProdutoServico _servico;

        public FinalizarPedidoControle(IProdutoServico servico) => _servico = servico;

        [HttpPost]
        public async Task<IActionResult> FinalizarPedido([FromBody] PedidoComando comando)
        {
            if (comando == null || comando.Itens == null || !comando.Itens.Any())
            {
                return BadRequest("O pedido deve conter pelo menos um item.");
            }

            await _servico.FinalizarPedidoAsync(comando);
            return Ok(new { mensagem = "Pedido finalizado com sucesso!" });
        }
    }
    public class PedidoComando
    {
        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }

    public class ItemPedidoDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}