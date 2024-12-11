using Rony.Store.Domain.DTOs.BaseDTO;

namespace Rony.Store.Domain.DTOs.Departments;
public class DepartmentDTO : NameDTO
{
    public int Id { get; set; }
    public List<SubDepartmentDTO> SubDepartments { get; set; }
}
