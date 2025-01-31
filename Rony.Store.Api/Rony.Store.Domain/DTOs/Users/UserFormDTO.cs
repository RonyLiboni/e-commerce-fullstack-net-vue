using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.Users;
public class UserFormDTO
{
    [Required]
    [MaxLength(200)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(200)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(200)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
}
