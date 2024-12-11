using Rony.Store.Domain.Entities.BaseEntities;

namespace Rony.Store.Domain.Entities.Departments;
public class Department : BaseEntityWithName
{
    public List<SubDepartment> SubDepartments { get; set; }
}