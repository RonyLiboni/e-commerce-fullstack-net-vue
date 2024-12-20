using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.Departments;
public class DepartmentFormDTO
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
}