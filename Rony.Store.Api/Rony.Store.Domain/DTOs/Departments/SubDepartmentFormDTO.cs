using Rony.Store.Domain.DTOs.BaseDTO;
using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.Departments;
public sealed class SubDepartmentFormDTO : NameDTO
{
    [Required]
    public int DepartmentId { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
}