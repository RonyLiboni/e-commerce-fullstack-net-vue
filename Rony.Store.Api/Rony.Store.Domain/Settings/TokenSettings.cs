namespace Rony.Store.Domain.Settings;
public class TokenSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string AccessTokenSecretKey { get; set; }
    public string RefreshTokenSecretKey { get; set; }
    public int AccessTokenDurationInMinutes { get; set; }
    public int RefreshTokenDurationInDays { get; set; }

}