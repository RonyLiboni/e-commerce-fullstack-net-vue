using Rony.Store.Domain.Entities.BaseCommons;

namespace Rony.Store.Domain.Entities.DepartmentHierarchy;
public class Department : BaseEntityWithName
{
    public IEnumerable<SubDepartment> SubDepartments { get; set; } = [];
}