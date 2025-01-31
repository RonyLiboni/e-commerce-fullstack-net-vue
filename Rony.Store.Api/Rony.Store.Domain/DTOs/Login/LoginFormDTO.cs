using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.Login;
public class LoginFormDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
