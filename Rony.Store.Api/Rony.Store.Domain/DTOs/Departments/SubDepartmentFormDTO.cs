using Rony.Store.Domain.DTOs.BaseDTO;

namespace Rony.Store.Domain.DTOs.Departments;
public sealed class SubDepartmentFormDTO : NameDTO
{
    public int DepartmentId { get; set; }
}