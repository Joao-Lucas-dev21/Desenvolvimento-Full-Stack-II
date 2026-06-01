using Atividade.Aplicacao.DTO;
using Atividade.Aplicacao.Serviços;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atividade.Api.Controle
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoControle : ControllerBase
    {
        private readonly IPedidoServico _servico;
        public PedidoControle(IPedidoServico servico) => _servico = servico;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _servico.GetAllAsync();
            return Ok(pedidos);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidoDTO dTO)
        {
            var pedido = await _servico.CreateAsync(dTO);
            return Ok(pedido);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PedidoDTO dTO)
        {
            var pedido = await _servico.UpdateAsync(id, dTO);
            return Ok(pedido);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _servico.DeleteAsync(id);
            return NoContent();

        }
    }
}
