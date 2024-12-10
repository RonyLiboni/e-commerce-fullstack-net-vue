using Rony.Store.Domain.Entities.BaseCommons;
using System.Text.Json.Serialization;

namespace Rony.Store.Domain.Entities.Departments;
public class Category : BaseEntityWithName
{
    [JsonIgnore]
    public SubDepartment SubDepartment { get; set; }
    public int SubDepartmentId { get; set; }

}
