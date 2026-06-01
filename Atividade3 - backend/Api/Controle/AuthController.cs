using Atividade.Aplicacao.DTO;
using Atividade.Aplicacao.Serviços;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Atividade.Api.Controle
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<IdentityUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = new IdentityUser
            {
                UserName = dTO.Email,
                Email = dTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dTO.Password);

            if (result.Succeeded)
            {
                return Ok("Usuário registrado com sucesso.");
            }

            return BadRequest(result.Errors);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isValid = await _authService.ValidateUser(model.email, model.password);

            if (!isValid)
            {
                return Unauthorized("Email ou senha incorretos.");
            }

            var user = await _userManager.FindByEmailAsync(model.email);

            if (user == null)
            {
                return Unauthorized("Usuário não encontrado.");
            }

            var token = await _authService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                userEmail = user.Email
            });
        }

    }
}
