using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Atividade.Aplicacao.Serviços;
using Atividade.Aplicacao.DTO;

namespace Atividade.Api.Controle
{
    [ApiController]
    [Route("api/produto")] 
    [Authorize] 
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoServico _produtoServico;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IProdutoServico produtoServico, ILogger<ProdutoController> logger)
        {
            _produtoServico = produtoServico;
            _logger = logger;
        }
            
        
        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var produtos = await _produtoServico.GetAllAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter produtos");
                return StatusCode(500, $"Erro interno ao tentar listar os produtos registrados");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                await _produtoServico.UpdateAsync(id, produtoDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar produto com id {id}");
                return StatusCode(500, "Erro interno ao tentar atualizar o produto");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _produtoServico.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar produto com id {id}");
                return StatusCode(500, "Erro interno ao tentar deletar o produto");
            }
        }
    }
}