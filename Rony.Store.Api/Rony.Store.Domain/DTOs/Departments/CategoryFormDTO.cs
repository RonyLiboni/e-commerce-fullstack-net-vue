using Rony.Store.Domain.DTOs.BaseDTO;

namespace Rony.Store.Domain.DTOs.Departments;
public class CategoryFormDTO : NameDTO
{
    public int SubDepartmentId { get; set; }
}
