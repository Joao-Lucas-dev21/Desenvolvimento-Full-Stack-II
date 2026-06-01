using Microsoft.AspNetCore.Identity;

namespace Atividade.Aplicacao.Serviços
{
    public interface IAuthService
    {
        Task<string> GenerateToken(IdentityUser user);
        Task<bool> ValidateUser(string email, string password);
    }
}
