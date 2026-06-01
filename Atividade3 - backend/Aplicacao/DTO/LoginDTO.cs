using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Atividade.Aplicacao.DTO
{
    public class LoginDTO
    {
        [Required]
        [JsonPropertyName("email")]
        public string email { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("password")] 
        public string password { get; set; } = string.Empty;
    }
}