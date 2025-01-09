using System.Text.Json.Serialization;

namespace Rony.Store.Domain.DTOs.Login;
public class LoginDTO
{
    public string AccessToken { get; set; }
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    [JsonIgnore]
    public DateTime? RefreshTokenExpiresAt { get; set; }
}
