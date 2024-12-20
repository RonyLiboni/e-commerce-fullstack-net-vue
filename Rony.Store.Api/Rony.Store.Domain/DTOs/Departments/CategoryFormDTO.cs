using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.Departments;
public class CategoryFormDTO
{
    [Required]
    public int SubDepartmentId { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
}
