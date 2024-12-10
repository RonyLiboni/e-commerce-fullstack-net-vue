using Rony.Store.Domain.Entities.BaseCommons;
using System.Text.Json.Serialization;

namespace Rony.Store.Domain.Entities.DepartmentHierarchy;
public class SubDepartment : BaseEntityWithName
{
    [JsonIgnore]
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
    public List<Category> Categories { get; set; } = [];
}
