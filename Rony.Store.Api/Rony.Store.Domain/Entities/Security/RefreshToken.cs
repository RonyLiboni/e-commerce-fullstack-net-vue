namespace Rony.Store.Domain.Entities.Security;
public class RefreshToken
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public RefreshToken()
    {  }
    public RefreshToken(Guid id, string email, DateTime expiresAt)
    {
        Id = id;
        Email = email;
        ExpiresAt = expiresAt;
    }
}
