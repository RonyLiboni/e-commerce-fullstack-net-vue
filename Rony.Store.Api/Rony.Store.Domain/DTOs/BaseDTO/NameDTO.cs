using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.BaseDTO;
public abstract class NameDTO
{
    [Required]
    public string Name { get; set; }
}