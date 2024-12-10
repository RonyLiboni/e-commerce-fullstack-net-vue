using Rony.Store.Domain.Entities.BaseCommons;

namespace Rony.Store.Domain.Entities.Departments;
public class Department : BaseEntityWithName
{
    public IEnumerable<SubDepartment> SubDepartments { get; set; } = [];
}