using Rony.Store.Domain.Entities.BaseEntities;

namespace Rony.Store.Domain.Entities.Security;
public class GroupRole : BaseEntityWithName
{
    public string Description { get; set; }
    public List<Role> Roles { get; set; }
}