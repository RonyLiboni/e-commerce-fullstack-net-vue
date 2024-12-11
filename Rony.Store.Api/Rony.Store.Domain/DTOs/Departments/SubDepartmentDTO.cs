using Rony.Store.Domain.DTOs.BaseDTO;

namespace Rony.Store.Domain.DTOs.Departments;
public class SubDepartmentDTO : NameDTO
{
    public int Id { get; set; }
    public List<CategoryDTO> Categories { get; set; }
}
