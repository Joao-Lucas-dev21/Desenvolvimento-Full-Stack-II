using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Atividade.Aplicacao.Serviços
{
    public class AuthService : IAuthService
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly IConfiguration _configuration;

            public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _configuration = configuration;
            }

            public async Task<bool> ValidateUser(string email, string password)
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user != null && await _userManager.CheckPasswordAsync(user, password);

            }


        public async Task<string> GenerateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // 1. Garante que se a chave não existir, o sistema avisa com clareza
            var jwtKey = _configuration["Jwt:Key"] ?? "ChaveDeSegurancaPadraoComMaisDe32CaracteresParaEvitarErros!!";
            var key = Encoding.ASCII.GetBytes(jwtKey);

            var roles = await _userManager.GetRolesAsync(user);

            // 2. Protege as claims usando o operador '??' para nunca passar null
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? "Usuario"),
        new Claim(ClaimTypes.Email, user.Email ?? "sem@email.com"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            foreach (var role in roles)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var tokensDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokensDescriptor);
            return tokenHandler.WriteToken(token);
            }
        }
    }